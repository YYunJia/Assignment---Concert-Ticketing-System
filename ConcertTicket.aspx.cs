using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ConcertTicketing
{
	public partial class ConcertTicket : System.Web.UI.Page
	{
        private string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                BindEvents();
            }
        }

        private void BindEvents()
        {
            string query = @"
                           SELECT e.eventNumericId, e.eventName, e.artistName, e.eventDateTime, 
                           e.imageUrl, v.venueName, v.location, e.eventDetails, e.eventStatus
                           FROM Event e
                           JOIN EventVenue ev ON e.eventId = ev.eventId
                           JOIN Venue v ON ev.venueId = v.venueId
                           ORDER BY e.eventDateTime";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    string dayOfWeek = ((DateTime)row["eventDateTime"]).ToString("ddd").ToUpper();
                    string day = ((DateTime)row["eventDateTime"]).ToString("dd");
                    string monthYear = ((DateTime)row["eventDateTime"]).ToString("MMM yyyy");
                    string time = ((DateTime)row["eventDateTime"]).ToString("HH:mm");

                    string eventName = row["eventName"].ToString();
                    string artist = row["artistName"].ToString();
                    string venueName = row["venueName"].ToString();
                    string location = row["location"].ToString();
                    string imageUrl = row["ImageUrl"].ToString();
                    string eventNumericId = row["eventNumericId"].ToString();

                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        imageUrl = "defaultImage.jpg"; 
                    }

                    string cardHtml = $@"
                   <div class='card' onclick='redirectToDetails({eventNumericId})'>
                   <div class='date'>
                   <span class='day'>{dayOfWeek}</span>
                   <span class='day-number'>{day}</span>
                   <span class='month-year'>{monthYear}</span>
                   <span class='time'>{time}</span>
                   </div>
                   <div class='image-container'>
                   <img src='{imageUrl}' alt='{eventName}' />
                   </div>
                   <div class='details'>
                   <h3>{eventName}</h3>
                   <p>{artist}</p>
                   <p><b>{venueName} | {location}</b></p>
                   </div>
                   <div class='ticket-button'>
                   <a href='Details.aspx?eventId={eventNumericId}' onclick='event.stopPropagation();'>Find Tickets</a>
                   </div>
                   </div>";

                    EventContainer.Controls.Add(new LiteralControl(cardHtml));
                }
            }
        }
    }
}
