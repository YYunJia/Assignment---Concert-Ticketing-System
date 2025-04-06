using System;
using System.Data.SqlClient;
using System.Configuration;

namespace WebAssignment
{
    public partial class confirmation : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the bookingId from the session
                string bookingId = Session["BookingId"] as string;

                if (string.IsNullOrEmpty(bookingId))
                {
                    // Handle the case where bookingId is not found in the session
                    lblCust.Text = "Booking information not found.";
                    return;
                }

                // Fetch booking details from the database
                FetchBookingDetails(bookingId);
            }
        }

        private void FetchBookingDetails(string bookingId)
        {
            // Step 1: Fetch the user's name from the Users table
            string userId = Session["UserID"] as string;
            string userName = GetUserName(userId);

            if (!string.IsNullOrEmpty(userName))
            {
                // Display the user's name in the "Dear [User Name]" section
                lblCust.Text = userName;
            }
            else
            {
                lblCust.Text = "User information not found.";
            }

            // Step 2: Fetch other booking details (if needed)
            string query = @"
                SELECT 
                    b.bookingDateTime, 
                    b.totalPrice, 
                    b.numOfTicket, 
                    c.customerId, 
                    e.eventName, 
                    e.eventDateTime, 
                    e.ImageUrl AS eventPoster, 
                    v.venueName, 
                    t.seatNo
                FROM 
                    Booking b
                    INNER JOIN Customer c ON b.customerId = c.customerId
                    INNER JOIN Event e ON b.eventId = e.eventId
                    INNER JOIN Venue v ON e.eventId = b.eventId
                    INNER JOIN Ticket t ON b.bookingId = t.bookingId
                WHERE 
                    b.bookingId = @bookingId
                    AND t.seatNo IS NOT NULL
                    AND t.seatNo NOT LIKE '%Not enough VIP seats available%'
                    AND t.seatNo NOT LIKE '%Available: %'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // You can still fetch the data here if needed for other purposes
                        // For example, logging or further processing
                    }
                    else
                    {
                        // Handle the case where no data is found
                        // You can add a message here if needed
                    }
                }
            }
        }

        private string GetUserName(string userId)
        {
            string userName = "";
            string query = "SELECT name FROM Users WHERE userId = @userId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userName = result.ToString();
                    }
                }
            }

            return userName;
        }
    }
}