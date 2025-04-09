using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConcertTicketing
{
	public partial class Booking : System.Web.UI.Page
	{
        private string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
		{
            // Check if the user is logged in
            if (Session["UserEmail"] == null || Session["UserRole"] == null || Session["UserID"] == null)
            {
                Response.Redirect("~/LoginPage.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    string eventId = Request.QueryString["eventid"];
                    if (string.IsNullOrEmpty(eventId))
                    {
                        Response.Redirect("Details.aspx");
                        return;
                    }

                    // Format the eventId to match the database format (E + numeric ID)
                    string formattedEventId = "E" + eventId.PadLeft(3, '0');

                    // Load event details and seating map
                    LoadEventDetails(formattedEventId);
                    LoadSeatCoordinatesAndPrices(formattedEventId); // Load seat coordinates and prices from the database
                }
            }
        }

        protected void btnProceedToPayment_Click(object sender, EventArgs e)
        {
            // Validate session
            if (Session["UserID"] == null)
            {
                ShowErrorMessage("User not logged in.");
                return;
            }

            string totalPrice = hdnTotalPrice.Value;
            int numOfTickets = int.Parse(hdnNumOfTickets.Value);
            string selectedSeats = hdnSelectedSeats.Value;
            string eventId = Request.QueryString["eventid"];

            // Format the eventId to match the database format (E + numeric ID)
            string formattedEventId = "E" + eventId.PadLeft(3, '0');

            // Retrieve venueId from the database
            string venueId = GetVenueIdByEventId(formattedEventId);

            if (string.IsNullOrEmpty(venueId))
            {
                ShowErrorMessage("VenueId not found for the selected event.");
                return;
            }

            // Consolidate vvip1 and vvip2 into vvip
            selectedSeats = ConsolidateVVIPSeats(selectedSeats);

            // Debugging: Log the formatted eventId and venueId
            System.Diagnostics.Debug.WriteLine($"Proceeding to payment with formatted eventId: {formattedEventId}, venueId: {venueId}");

            // Save booking details to the database
            string customerId = Session["UserID"].ToString(); // Use the logged-in user's ID
            string paymentId = "P001"; // Replace with actual payment ID

            // Check if the booking already exists
            if (BookingExists(customerId, formattedEventId))
            {
                ShowErrorMessage("You have already booked this event.");
                return;
            }

            // Save the booking
            SaveBooking(totalPrice, numOfTickets, customerId, paymentId, formattedEventId);

            // Redirect to the checkout page
            Response.Redirect($"Checkout.aspx?totalPrice={totalPrice}&eventId={formattedEventId}&selectedSeats={selectedSeats}&venueId={venueId}");
        }

        // Method to consolidate vvip1 and vvip2 into vvip
        private string ConsolidateVVIPSeats(string selectedSeats)
        {
            string[] seats = selectedSeats.Split(',');
            int vvipCount = 0;

            // Iterate through the selected seats and count vvip1 and vvip2
            for (int i = 0; i < seats.Length; i++)
            {
                string[] parts = seats[i].Split(':');
                string category = parts[0].ToLower();
                int count = int.Parse(parts[1]);

                if (category == "vvip1" || category == "vvip2")
                {
                    vvipCount += count;
                    seats[i] = null; // Mark vvip1 and vvip2 for removal
                }
            }

            // Remove null entries from the array
            seats = seats.Where(s => s != null).ToArray();

            // Add the consolidated vvip category
            if (vvipCount > 0)
            {
                Array.Resize(ref seats, seats.Length + 1);
                seats[seats.Length - 1] = $"vvip:{vvipCount}";
            }

            // Join the array back into a string
            return string.Join(",", seats);
        }

        private string GetVenueIdByEventId(string eventId)
        {
            string venueId = "";
            string query = @"
                SELECT v.venueId 
                FROM Event e
                JOIN EventVenue ev ON e.eventId = ev.eventId
                JOIN Venue v ON ev.venueId = v.venueId
                WHERE e.eventId = @eventId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        venueId = result.ToString();
                    }
                }
            }

            return venueId;
        }

        private void ShowErrorMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showError", $"alert('{message}');", true);
        }

        private bool EventExists(string eventId)
        {
            string query = "SELECT COUNT(*) FROM Event WHERE eventId = @eventId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private bool BookingExists(string customerId, string eventId)
        {
            string query = "SELECT COUNT(*) FROM Booking WHERE customerId = @customerId AND eventId = @eventId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void SaveBooking(string totalPrice, int numOfTickets, string customerId, string paymentId, string eventId)
        {
            try
            {
                if (!EventExists(eventId))
                {
                    throw new Exception($"Event with ID {eventId} does not exist.");
                }

                string query = @"
                    INSERT INTO Booking (bookingDateTime, totalPrice, numOfTicket, customerId, paymentId, eventId)
                    VALUES (@bookingDateTime, @totalPrice, @numOfTicket, @customerId, @paymentId, @eventId)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@bookingDateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                        cmd.Parameters.AddWithValue("@numOfTicket", numOfTickets);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@paymentId", paymentId);
                        cmd.Parameters.AddWithValue("@eventId", eventId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving booking: {ex.Message}");
                ShowErrorMessage("An error occurred while saving the booking. Please try again.");
            }
        }

        private void LoadEventDetails(string eventId)
        {
            string query = @"
                SELECT e.eventName, v.location, e.eventDateTime, e.seatselectURL 
                FROM Event e
                JOIN EventVenue ev ON e.eventId = ev.eventId
                JOIN Venue v ON ev.venueId = v.venueId
                WHERE e.eventId = @eventId";

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
                            // Set event details
                            lblEventName.Text = reader["eventName"].ToString();
                            lblVenueLocation.Text = reader["location"].ToString();
                            lblEventDate.Text = Convert.ToDateTime(reader["eventDateTime"]).ToString("dd MMM yyyy");

                            // Set the seating map image URL dynamically
                            string seatselectURL = reader["seatselectURL"]?.ToString();
                            if (string.IsNullOrEmpty(seatselectURL))
                            {
                                imgEventMap.Visible = false; // Hide the image control
                            }
                            else
                            {
                                seatselectURL = seatselectURL.Replace("(", "%28").Replace(")", "%29"); // Encode parentheses
                                imgEventMap.ImageUrl = ResolveUrl("~" + seatselectURL); // Use ResolveUrl
                            }
                        }
                    }
                }
            }
        }

        private void LoadSeatCoordinatesAndPrices(string eventId)
        {
            string query = @"
        SELECT s.category, s.price, e.seatcoorstandard, e.seatcoorvip, e.seatcoorvvip1, e.seatcoorvvip2
        FROM Seat s
        JOIN Event e ON s.eventId = e.eventId
        WHERE s.eventId = @eventId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@eventId", eventId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Initialize variables to store seat data
                        string standardCoords = "";
                        int standardPrice = 0;
                        string vipCoords = "";
                        int vipPrice = 0;
                        string vvip1Coords = "";
                        string vvip2Coords = "";
                        int vvipPrice = 0;

                        while (reader.Read())
                        {
                            string category = reader["category"].ToString().ToLower();
                            int price = Convert.ToInt32(reader["price"]);

                            if (category == "standard")
                            {
                                standardCoords = reader["seatcoorstandard"].ToString();
                                standardPrice = price;
                            }
                            else if (category == "vip")
                            {
                                vipCoords = reader["seatcoorvip"].ToString();
                                vipPrice = price;
                            }
                            else if (category == "vvip")
                            {
                                vvip1Coords = reader["seatcoorvvip1"].ToString();
                                vvip2Coords = reader["seatcoorvvip2"].ToString();
                                vvipPrice = price;
                            }
                        }

                        // Create the seatData object with all properties initialized
                        var seatData = new
                        {
                            standard = new { coords = standardCoords, price = standardPrice },
                            vip = new { coords = vipCoords, price = vipPrice },
                            vvip1 = new { coords = vvip1Coords, price = vvipPrice },
                            vvip2 = new { coords = vvip2Coords, price = vvipPrice }
                        };

                        // Debugging: Log the seatData object
                        System.Diagnostics.Debug.WriteLine("Seat Data: " + new JavaScriptSerializer().Serialize(seatData));

                        // Serialize seat data to JSON and pass it to the frontend
                        string seatDataJson = new JavaScriptSerializer().Serialize(seatData);
                        string script = $"<script type='text/javascript'>var seatData = {seatDataJson};</script>";
                        LiteralControl literal = new LiteralControl(script);
                        Page.Header.Controls.Add(literal);
                    }
                }
            }
        }
    }
}
