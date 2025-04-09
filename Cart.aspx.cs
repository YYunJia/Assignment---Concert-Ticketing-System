using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConcertTicketing
{
	public partial class Cart : System.Web.UI.Page
	{
        private string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                // Retrieve the total price, event ID, and selected seats from the query string
                string totalPrice = Request.QueryString["totalPrice"];
                string eventId = Request.QueryString["eventId"];
                string selectedSeats = Request.QueryString["selectedSeats"];

                if (string.IsNullOrEmpty(totalPrice) || string.IsNullOrEmpty(eventId) || string.IsNullOrEmpty(selectedSeats))
                {
                    // Redirect to an error page or the previous page if data is missing
                    Response.Redirect("Booking.aspx");
                    return;
                }

                // Fetch event details from the database
                var eventDetails = GetEventDetails(eventId);
                if (eventDetails == null)
                {
                    // Handle the case where the event is not found
                    Response.Redirect("MainPage.aspx");
                    return;
                }

                // Display the event name and total price on the page
                lblEventName.Text = eventDetails.EventName;

                // Calculate and display the booking fee and operational fee
                decimal subtotal = decimal.Parse(totalPrice.Replace("RM", ""));
                decimal bookingFee = subtotal * 0.01m;
                decimal operationalFee = subtotal * 0.03m;
                decimal total = subtotal + bookingFee + operationalFee;

                lblSubtotal.Text = "RM" + subtotal.ToString("F2");
                lblBookingFee.Text = "RM" + bookingFee.ToString("F2");
                lblOperationalFee.Text = "RM" + operationalFee.ToString("F2");
                lblTotalPrice.Text = "RM" + total.ToString("F2");

                // Display the event image
                imgEvent.ImageUrl = eventDetails.ImageUrl;

                // Display the randomly allocated seat numbers or error messages
                lblSeatDetails.Text = GetRandomSeatNumbers(eventId, selectedSeats);
            }
        }

        private EventDetails GetEventDetails(string eventId)
        {
            EventDetails eventDetails = null;
            string query = "SELECT eventName, ImageUrl FROM Event WHERE eventId = @eventId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            eventDetails = new EventDetails
                            {
                                EventName = reader["eventName"].ToString(),
                                ImageUrl = reader["ImageUrl"].ToString()
                            };
                        }
                    }
                }
            }

            return eventDetails;
        }

        private string GetRandomSeatNumbers(string eventId, string selectedSeats)
        {
            StringBuilder seatDetails = new StringBuilder();
            string[] seats = selectedSeats.Split(',');

            foreach (string seat in seats)
            {
                string[] parts = seat.Split(':');
                string category = parts[0];
                int count = int.Parse(parts[1]);

                // Check if there are enough available seats
                int availableSeats = GetAvailableSeatCount(eventId, category);
                if (availableSeats < count)
                {
                    seatDetails.Append($"<div style='color: red;'>Not enough {category.ToUpper()} seats available. Requested: {count}, Available: {availableSeats}</div>");
                }
                else
                {
                    // Fetch random seat numbers for the given category and count
                    string seatNumbers = GetRandomSeatsFromDatabase(eventId, category, count);
                    seatDetails.Append($"{category.ToUpper()}:{seatNumbers};"); // Use a structured format
                }
            }

            return seatDetails.ToString();
        }

        private int GetAvailableSeatCount(string eventId, string category)
        {
            int availableSeats = 0;
            string query = "SELECT COUNT(*) FROM Seat WHERE eventId = @eventId AND category = @category AND status = 'Available'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    cmd.Parameters.AddWithValue("@category", category);
                    conn.Open();
                    availableSeats = (int)cmd.ExecuteScalar();
                }
            }

            return availableSeats;
        }

        private string GetRandomSeatsFromDatabase(string eventId, string category, int count)
        {
            StringBuilder seatNumbers = new StringBuilder();
            string query = @"
SELECT TOP (@count) seatNo 
FROM Seat 
WHERE eventId = @eventId AND (category = @category OR category = 'vvip1' OR category = 'vvip2') AND status = 'Available' 
ORDER BY NEWID()";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@count", count);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string seatNo = reader["seatNo"].ToString();
                            seatNumbers.Append(seatNo + ", ");
                        }
                    }
                }
            }

            // Remove the trailing comma and space
            if (seatNumbers.Length > 0)
            {
                seatNumbers.Length -= 2;
            }

            return seatNumbers.ToString();
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            // Retrieve the event name and total price from the labels
            string eventName = lblEventName.Text;
            string subtotal = lblSubtotal.Text.Replace("RM", "").Trim();
            string bookingFee = lblBookingFee.Text.Replace("RM", "").Trim();
            string operationalFee = lblOperationalFee.Text.Replace("RM", "").Trim();
            string totalAmount = lblTotalPrice.Text.Replace("RM", "").Trim();

            // Retrieve the allocated seat numbers from the label
            string allocatedSeats = lblSeatDetails.Text; // Ensure this contains the seat numbers

            // Clean the allocatedSeats value: Remove HTML tags and extract seat numbers
            allocatedSeats = Regex.Replace(allocatedSeats, "<.*?>", string.Empty).Trim();

            // Debug: Log the cleaned allocatedSeats value
            System.Diagnostics.Debug.WriteLine("Cleaned Allocated Seats: " + allocatedSeats);

            // Retrieve eventId and venueId from the query string
            string eventId = Request.QueryString["eventId"];
            string venueId = Request.QueryString["venueId"];

            // Debug: Check if eventId and venueId are retrieved correctly
            System.Diagnostics.Debug.WriteLine("EventId from Query String: " + eventId);
            System.Diagnostics.Debug.WriteLine("VenueId from Query String: " + venueId);

            if (string.IsNullOrEmpty(eventId) || string.IsNullOrEmpty(venueId))
            {
                ShowErrorMessage("EventId or VenueId is missing.");
                return;
            }

            // Store data in session variables
            Session["EventName"] = eventName;
            Session["Subtotal"] = subtotal;
            Session["BookingFee"] = bookingFee;
            Session["OperationFee"] = operationalFee;
            Session["TotalAmount"] = totalAmount;
            Session["AllocatedSeats"] = allocatedSeats; // Store cleaned allocated seats in session
            Session["EventId"] = eventId; // Store eventId in session
            Session["VenueId"] = venueId; // Store venueId in session

            // Redirect to Payment page
            Response.Redirect("Payment.aspx");
        }

        // Method to show error messages using JavaScript alerts
        private void ShowErrorMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showError", $"alert('{message}');", true);
        }
    }

    public class EventDetails
    {
        public string EventName { get; set; }
        public string ImageUrl { get; set; }
    }
}
