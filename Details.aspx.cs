using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace WebAssignment
{
    public partial class Details : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ConnectionString;
        private string eventId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                eventId = Request.QueryString["eventId"];
                System.Diagnostics.Debug.WriteLine("Event ID from query string: " + eventId);  // Log the event ID

                if (string.IsNullOrEmpty(eventId))
                {
                    eventDetailsContainer.InnerHtml = "<p>No event selected.</p>";
                    return; // Exit the method
                }

                // Format the eventId to match the database format (E + numeric ID)
                string formattedEventId = "E" + eventId.PadLeft(3, '0');
                System.Diagnostics.Debug.WriteLine("Formatted Event ID: " + formattedEventId);

                try
                {
                    var eventDetails = GetEventDetails(formattedEventId);

                    if (eventDetails != null)
                    {
                        DisplayEventDetails(eventDetails);
                        DisplayPricingInformation(formattedEventId); // Call the method to display pricing
                    }
                    else
                    {
                        eventDetailsContainer.InnerHtml = "<p>Event not found.</p>";
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error loading event details: " + ex.Message);
                    eventDetailsContainer.InnerHtml = "<p>An error occurred while loading event details.</p>";
                }
            }
        }

        protected void btnBuyTicket_Click(object sender, EventArgs e)
        {
            // Retrieve the eventId from the query string
            string eventId = Request.QueryString["eventId"];

            if (string.IsNullOrEmpty(eventId))
            {
                // Handle the case where eventId is not available
                Response.Redirect("Details.aspx"); // Redirect back to the details page or an error page
                return;
            }

            // Redirect to Booking.aspx with the eventId as a query string parameter
            Response.Redirect($"Booking.aspx?eventId={eventId}");
        }

        private DataRow GetEventDetails(string eventId)
        {
            string query = @"
SELECT e.eventName, e.artistName, e.eventDateTime, v.venueName, v.location, 
       e.eventDetails, e.eventStatus, e.DetailImageURL
FROM Event e
JOIN EventVenue ev ON e.eventId = ev.eventId
JOIN Venue v ON ev.venueId = v.venueId
WHERE e.eventId = @EventId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EventId", eventId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        private void DisplayEventDetails(DataRow eventDetails)
        {
            if (eventDetails != null)
            {
                string eventName = eventDetails["eventName"]?.ToString() ?? "N/A";
                string artist = eventDetails["artistName"]?.ToString() ?? "N/A";
                string venueName = eventDetails["venueName"]?.ToString() ?? "N/A";
                string location = eventDetails["location"]?.ToString() ?? "N/A";
                string eventDateTime = eventDetails["eventDateTime"] != DBNull.Value
                    ? ((DateTime)eventDetails["eventDateTime"]).ToString("dddd, MMMM dd, yyyy HH:mm")
                    : "N/A";
                string eventDetailsText = eventDetails["eventDetails"]?.ToString() ?? "N/A";
                string detailImageUrl = eventDetails["DetailImageURL"]?.ToString() ?? string.Empty;

                // HTML structure for the event details
                string eventHtml = $@"
<div style='width: 100%; text-align: center; margin-bottom: 30px; margin-top: 20px;'>
    <img src='{ResolveUrl(detailImageUrl)}' alt='{eventName}' style='max-width: 100%; height: auto; border-radius: 10px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);' />
</div>
<div style='padding: 20px; background-color: #f9f9f9; border-radius: 10px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);'>
    <h1 style='font-size: 2.5em; color: #333; margin-bottom: 20px;'>{eventName}</h1>
    <p style='font-size: 1.2em; color: #555; margin-bottom: 10px;'><b>Artist:</b> <span style='color: #007BFF;'>{artist}</span></p>
    <p style='font-size: 1.2em; color: #555; margin-bottom: 10px;'><b>Venue:</b> {venueName} | <span style='color: #007BFF;'>{location}</span></p>
    <p style='font-size: 1.2em; color: #555; margin-bottom: 10px;'><b>Date & Time:</b> <span style='color: #28a745;'>{eventDateTime}</span></p>
    <p style='font-size: 1.1em; color: #666; line-height: 1.6;'><b>Details:</b> {eventDetailsText}</p>
</div>";

                eventDetailsContainer.InnerHtml = eventHtml;
            }
        }

        private void DisplayPricingInformation(string eventId)
        {
            // Query to get distinct categories and prices for the event
            string query = @"
SELECT DISTINCT category, price
FROM Seat
WHERE eventId = @EventId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EventId", eventId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        string pricingHtml = "";
                        foreach (DataRow row in dt.Rows)
                        {
                            string category = row["category"].ToString();
                            string price = row["price"].ToString();
                            pricingHtml += $@"
<div class='price-row'>
    <h3>{category}</h3>
    <p class='price'>RM{price}</p>
</div>";
                        }
                        pricingContainer.InnerHtml = pricingHtml;
                    }
                    else
                    {
                        pricingContainer.InnerHtml = "<p>No pricing information available.</p>";
                    }
                }
            }
        }
    }
}