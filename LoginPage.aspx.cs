using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConcertTicketing
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

            // Attempt to get the user's role based on their credentials
            string role = ValidateUserCredentialsAndGetRole(email, password);

            if (!string.IsNullOrEmpty(role))
            {
                // Set session variables for authenticated user
                Session["UserEmail"] = email; // Store user email for session
                Session["UserRole"] = role;   // Store role for authorization checks
                Session["UserLoggedIn"] = true; // Indicate that the user is logged in

                // Retrieve user ID and name, and set session variables
                string userId = GetUserIdByEmail(email);
                Session["UserID"] = userId;
                Session["UserName"] = GetUserNameById(userId);

                // Redirect based on the user's role
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
                // Use JavaScript alert for invalid login
                string errorMessage = "Invalid email or password. Please try again.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{errorMessage}');", true);
            }
        }
    }
}