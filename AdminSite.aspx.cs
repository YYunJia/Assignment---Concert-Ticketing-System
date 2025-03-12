using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConcertTicketing
{
    public partial class AdminSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Staff")
            {
                Response.Redirect("LoginPage.aspx");
            }
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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string contact = txtContact.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string role = "Staff"; // Default role is Staff
            string position = ddlPosition.SelectedValue; // Get the selected position


            // Check if password and confirm password match
            if (password != confirmPassword)
            {
                Response.Write("<script>alert('Passwords do not match.');</script>");
                return;
            }

            // Check if email already exists in the database
            if (EmailExists(email))
            {
                Response.Write("<script>alert('This email is already registered.');</script>");
                return;
            }

            // Validate contact number format using the updated regex
            if (!System.Text.RegularExpressions.Regex.IsMatch(contact, @"^\d{3}-?\d{7,10}$"))
            {
                Response.Write("<script>alert('Invalid contact number. Please enter in the format (010-1234567).');</script>");
                return;
            }

            // Hash the password
            string hashedPassword = HashPassword(password);

            // Register the user in the database
            bool registrationSuccessful = RegisterUser(name, email, contact, hashedPassword, role, position);
            if (registrationSuccessful)
            {
                // Show success message and redirect using JavaScript
                string successScript = "alert('Registration successful!);";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", successScript, true);
            }
            else
            {
                Response.Write("<script>alert('There was an error registering account. Please try again.');</script>");
            }
        }

        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            // Redirect to the dashboard page without triggering validation
            Response.Redirect("StaffDashboard.aspx");
        }

        private bool EmailExists(string email)
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

        private bool RegisterUser(string name, string email, string contact, string hashedPassword, string role, string position)
        {
            bool success = false;
            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    // Step 1: Insert the user into the Users table
                    string insertUserQuery = "INSERT INTO Users (Name, Email, Contact, Password, Role) VALUES (@Name, @Email, @Contact, @Password, @Role);";
                    SqlCommand cmd = new SqlCommand(insertUserQuery, con);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", role);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // If no rows are affected, something went wrong
                    if (rowsAffected == 0)
                    {
                        Response.Write("<script>alert('No rows affected in Users table, registration failed.');</script>");
                        return false;
                    }

                    // Step 2: Try using a different method to retrieve the user ID
                    string getUserIdQuery = "SELECT userId FROM Users WHERE Email = @Email;";
                    SqlCommand getUserIdCmd = new SqlCommand(getUserIdQuery, con);
                    getUserIdCmd.Parameters.AddWithValue("@Email", email);
                    object result = getUserIdCmd.ExecuteScalar();

                    // Check if we successfully retrieved the userId
                    if (result == DBNull.Value || result == null)
                    {
                        Response.Write("<script>alert('Failed to retrieve user ID from Users table.');</script>");
                        return false;
                    }

                    string userId = result.ToString();

                    // Step 3: Insert into Customer table using the retrieved userId
                    string insertCustomerQuery = "INSERT INTO Customer (userId, pointEarned) VALUES (@UserId, @PointEarned);";
                    SqlCommand insertCustomerCmd = new SqlCommand(insertCustomerQuery, con);
                    insertCustomerCmd.Parameters.AddWithValue("@UserId", userId);
                    insertCustomerCmd.Parameters.AddWithValue("@PointEarned", 0); // Default pointEarned to 0
                    insertCustomerCmd.ExecuteNonQuery();

                    // If role is staff, insert into Staff table
                    if (role.ToLower() == "staff")
                    {
                        string insertStaffQuery = "INSERT INTO Staff (userId, position) VALUES (@UserId, @Position);";
                        SqlCommand insertStaffCmd = new SqlCommand(insertStaffQuery, con);
                        insertStaffCmd.Parameters.AddWithValue("@UserId", userId);
                        insertStaffCmd.Parameters.AddWithValue("@Position", position);
                        insertStaffCmd.ExecuteNonQuery();
                    }

                    // Mark the registration as successful
                    success = true;
                }
                catch (Exception ex)
                {
                    // Log any exceptions to help with debugging
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
                finally
                {
                    con.Close();
                }
            }
            return success;
        }

    }



}
    }