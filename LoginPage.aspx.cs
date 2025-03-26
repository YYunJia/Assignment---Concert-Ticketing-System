using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

namespace ConcertTicketing
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optionally handle session or page-specific setup here
        }

        // Method to hash the password 
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert byte to hex
                }
                return builder.ToString();
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPwd.Text.Trim();

            // First, check if the email exists in the database
            if (!IsEmailRegistered(email))
            {
                string errorMessage = "The entered email is not registered. Please sign up or check your email.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{errorMessage}');", true);
                return; // Stop execution here if the email is not registered
            }

            // Attempt to get the user's role based on their credentials
            string role = ValidateUserCredentialsAndGetRole(email, password);

            if (!string.IsNullOrEmpty(role))
            {
                // Set session variables for authenticated user
                Session["UserEmail"] = email;
                Session["UserRole"] = role;
                Session["UserLoggedIn"] = true;

                string userId = GetUserIdByEmail(email);
                Session["UserID"] = userId;
                Session["UserName"] = GetUserNameById(userId);

                // Redirect based on role
                if (role == "Customer")
                {
                    Response.Redirect("MainPage.aspx");
                }
                else if (role == "Staff")
                {
                    Response.Redirect("StaffDashboard.aspx");
                }
            }
            else
            {
                // Incorrect password error
                string errorMessage = "Invalid password. Please try again.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{errorMessage}');", true);
            }
        }

        private bool IsEmailRegistered(string email)
        {
            bool exists = false;
            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                exists = count > 0;
            }

            return exists;
        }


        private string GetUserIdByEmail(string email)
        {
            string userId = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    userId = result.ToString();
                }
            }

            return userId;
        }

        private string GetUserNameById(string userId)
        {
            string userName = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    userName = result.ToString();
                }
            }

            return userName;
        }


        private string ValidateUserCredentialsAndGetRole(string email, string password)
        {
            string role = null; // Null indicates invalid credentials

            string hashedPassword = HashPassword(password); // Hash the input password
            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Role FROM Users WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", hashedPassword); // Hash the password in production

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // If user is found, retrieve their role
                    role = reader["Role"].ToString();
                }

                reader.Close();
            }

            return role;
        }
    }
}