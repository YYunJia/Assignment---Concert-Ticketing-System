using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConcertTicketing
{
    public partial class Profile : Page
    {
        protected string userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null || Session["UserRole"] == null || Session["UserID"] == null)
            {
                // If the user is not logged in, redirect to the login page
                Response.Redirect("~/LoginPage.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    userId = Session["UserID"].ToString();

                    // Display the user's name (retrieved from session)
                    lblWelcomeName.Text = Session["UserName"].ToString(); // This will show the user's name

                    // Directly load the user profile regardless of role
                    LoadUserProfile();
                }
            }
        }

        private void LoadUserProfile()
        {
            // Retrieve the userId from session (or wherever it's stored)
            string userId = Session["UserID"]?.ToString();  // Add null-safe check for Session["UserID"]

            if (string.IsNullOrEmpty(userId))
            {
                // Handle the case when userId is not available in session
                lblMsg.Text = "User not logged in!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;  // Stop execution if userId is missing
            }

            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Query to join Users and Customer tables to get the necessary information
                string query = @"
            SELECT 
                U.UserId, 
                U.Email, 
                U.Name, 
                U.Contact, 
                U.CreatedAt, 
                C.PointEarned 
            FROM 
                Users U
            INNER JOIN 
                Customer C ON U.UserId = C.UserId
            WHERE 
                U.UserId = @UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);  // Ensure @UserId is being added correctly

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblWelcomeName.Text = reader["Name"].ToString();
                    txtName.Text = reader["Name"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtContact.Text = reader["Contact"].ToString();
                    lblJoinDate.Text = ((DateTime)reader["CreatedAt"]).ToString("MM/dd/yyyy");

                    // Calculate days as a member
                    DateTime joinDate = (DateTime)reader["CreatedAt"];
                    int membershipDays = (DateTime.Now - joinDate).Days;
                    lblMembershipDays.Text = $"{membershipDays} days";

                    // Points and tier
                    int points = reader["PointEarned"] != DBNull.Value ? Convert.ToInt32(reader["PointEarned"]) : 0;
                    lblPointsEarned.Text = points.ToString();
                    lblMembershipTier.Text = GetMembershipTier(points);
                }
                reader.Close();
            }
        }

        private string GetMembershipTier(int points)
        {
            if (points >= 300) return "Gold";
            if (points >= 200) return "Silver";
            if (points >= 100) return "Bronze";
            return "Basic";
        }

        protected void LoadPurchaseHistory()
        {
            // Retrieve the userId from the session
            string userId = Session["UserID"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                // Handle the case when userId is not available in session
                lblNoPurchase.Text = "User not logged in!";
                lblNoPurchase.Visible = true;
                gvPurchaseHistory.Visible = false;
                return;
            }

            string query = @"
        SELECT 
            B.BookingId, 
            B.BookingDateTime AS BookingDate, 
            E.EventName, 
            B.NumOfTicket AS Quantity, 
            B.TotalPrice AS Amount,
            T.TicketId, 
            T.SeatNo AS SeatNumber,  
            'Standard' AS TicketType -- Replace with actual logic to determine ticket type
        FROM 
            Booking B
        INNER JOIN 
            Customer C ON B.CustomerId = C.CustomerId
        INNER JOIN 
            Event E ON B.EventId = E.EventId
        INNER JOIN 
            Ticket T ON B.BookingId = T.BookingId
        WHERE 
            C.UserId = @UserId
        ORDER BY 
            B.BookingDateTime DESC";

            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Bind the data to the GridView
                    gvPurchaseHistory.DataSource = dt;
                    gvPurchaseHistory.DataBind();
                    lblNoPurchase.Visible = false;
                    gvPurchaseHistory.Visible = true;
                }
                else
                {
                    // No purchase history found
                    gvPurchaseHistory.Visible = false;
                    lblNoPurchase.Text = "No purchase history found.";
                    lblNoPurchase.Visible = true;
                }
            }
        }

        protected void gvPurchaseHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewTicket")
            {
                string bookingId = e.CommandArgument.ToString();
                // Redirect to the ticket page with the BookingId as a query parameter
                Response.Redirect($"ticket.aspx?BookingId={bookingId}");
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true;
            txtContact.Enabled = true;
            txtEmail.Enabled = true;
            btnEdit.Visible = false;
            btnSave.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Retrieve the userId from session (or wherever it's stored)
            string userId = Session["UserID"].ToString(); // Make sure to handle this properly in case session is null

            // Validation before saving
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblMsg.Text = "Name cannot be empty.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Validate contact number format using the updated regex
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtContact.Text, @"^\d{10,11}$"))
            {
                Response.Write("<script>alert('Invalid contact number. Please enter a valid contact number (10-11 digits).');</script>");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                lblMsg.Text = "Invalid email format.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Check if email is already in use by another user
                string emailCheckQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND UserId != @UserId";
                SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, con);
                emailCheckCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                emailCheckCmd.Parameters.AddWithValue("@UserId", userId);  // Ensure the userId is added here

                con.Open();
                int emailCount = (int)emailCheckCmd.ExecuteScalar();
                if (emailCount > 0)
                {
                    lblMsg.Text = "The email is already in use by another account.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    con.Close();
                    return;
                }

                // Update the user information
                string updateQuery = "UPDATE Users SET Name = @Name, Contact = @Contact, Email = @Email WHERE UserId = @UserId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@Name", txtName.Text);
                updateCmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                updateCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                updateCmd.Parameters.AddWithValue("@UserId", userId);  // Ensure the userId is added here

                int rowsAffected = updateCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    lblMsg.Text = "Profile updated successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Error updating profile.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

                con.Close();
            }

            // Disable editing and reload the profile
            txtName.Enabled = false;
            txtContact.Enabled = false;
            txtEmail.Enabled = false;
            btnEdit.Visible = true;
            btnSave.Visible = false;

            LoadUserProfile();
        }

        protected void ShowPersonalInfo(object sender, EventArgs e)
        {
            personalInfoSection.Style["display"] = "block";
            membershipSection.Style["display"] = "none";
            purchaseHistorySection.Style["display"] = "none";
            changePasswordSection.Style["display"] = "none";
        }

        protected void ShowMembership(object sender, EventArgs e)
        {
            personalInfoSection.Style["display"] = "none";
            membershipSection.Style["display"] = "block";
            purchaseHistorySection.Style["display"] = "none";
            changePasswordSection.Style["display"] = "none";
        }

        protected void ShowPurchaseHistory(object sender, EventArgs e)
        {
            personalInfoSection.Style["display"] = "none";
            membershipSection.Style["display"] = "none";
            purchaseHistorySection.Style["display"] = "block";
            changePasswordSection.Style["display"] = "none";

            // Load purchase history when the section is viewed
            LoadPurchaseHistory();
        }

        protected void ShowChangePassword(object sender, EventArgs e)
        {
            personalInfoSection.Style["display"] = "none";
            membershipSection.Style["display"] = "none";
            purchaseHistorySection.Style["display"] = "none";
            changePasswordSection.Style["display"] = "block";
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Retrieve the userId from session (or wherever it's stored)
            string userId = Session["UserID"].ToString(); // Make sure to handle this properly in case session is null

            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmNewPassword.Text;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                lblChangePasswordMsg.Text = "All fields are required.";
                lblChangePasswordMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblChangePasswordMsg.Text = "New password and confirm password do not match.";
                lblChangePasswordMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (newPassword.Length < 8)
            {
                lblChangePasswordMsg.Text = "New password must be at least 8 characters long.";
                lblChangePasswordMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Hash the current password entered by the user
                string hashedCurrentPassword = HashPassword(currentPassword);

                // Verify current password (compare the hashed value)
                string passwordCheckQuery = "SELECT Password FROM Users WHERE UserID = @UserID";
                SqlCommand passwordCheckCmd = new SqlCommand(passwordCheckQuery, con);
                passwordCheckCmd.Parameters.AddWithValue("@UserID", userId);

                object result = passwordCheckCmd.ExecuteScalar();
                if (result == null || result.ToString() != hashedCurrentPassword)
                {
                    lblChangePasswordMsg.Text = "Current password is incorrect.";
                    lblChangePasswordMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Hash the new password before updating
                string hashedNewPassword = HashPassword(newPassword);

                // Update the new password
                string updatePasswordQuery = "UPDATE Users SET Password = @NewPassword WHERE UserID = @UserID";
                SqlCommand updatePasswordCmd = new SqlCommand(updatePasswordQuery, con);
                updatePasswordCmd.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                updatePasswordCmd.Parameters.AddWithValue("@UserID", userId);

                int rowsAffected = updatePasswordCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    lblChangePasswordMsg.Text = "Password changed successfully!";
                    lblChangePasswordMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblChangePasswordMsg.Text = "Error changing password.";
                    lblChangePasswordMsg.ForeColor = System.Drawing.Color.Red;
                }
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
    }
}