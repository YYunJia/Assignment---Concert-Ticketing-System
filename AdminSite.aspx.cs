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


        }
    }