﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConcertTicketing
{
    public partial class CreateEvent : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindVenueDropdown();
            }
        }

        private void BindVenueDropdown()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT venueId, venueName FROM Venue";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlVenue.DataSource = dt;
                    ddlVenue.DataTextField = "venueName";
                    ddlVenue.DataValueField = "venueId";
                    ddlVenue.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            if (!DateTime.TryParse(txtEventDateTime.Text, out DateTime eventDateTime) || eventDateTime < DateTime.Now)
            {
                resultMessage.InnerText = "Event date must be a valid future date.";
                return;
            }

            if (!int.TryParse(txtVVIPQuantity.Text, out int vvipQty) || vvipQty < 0 ||
                !decimal.TryParse(txtVVIPPrice.Text, out decimal vvipPrice) || vvipPrice < 0 ||
                !int.TryParse(txtVIPQuantity.Text, out int vipQty) || vipQty < 0 ||
                !decimal.TryParse(txtVIPPrice.Text, out decimal vipPrice) || vipPrice < 0 ||
                !int.TryParse(txtStandardQuantity.Text, out int standardQty) || standardQty < 0 ||
                !decimal.TryParse(txtStandardPrice.Text, out decimal standardPrice) || standardPrice < 0)
            {
                resultMessage.InnerText = "Invalid seat quantity or price.";
                return;
            }

            // Image uploads
            string imageUrl = UploadImage(fileUploadImage);
            string detailImageUrl = UploadImage(fileUploadDetailImage);
            string seatSelectUrl = UploadImage(fileUploadSeatSelectImage);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    string insertEventQuery = @"
                        INSERT INTO [dbo].[Event] ([eventName], [eventDateTime], [artistName], [eventDetails], [eventStatus], [ImageUrl], [DetailImageUrl], [seatselectURL]) 
                        VALUES (@eventName, @eventDateTime, @artistName, @eventDetails, @eventStatus, @imageUrl, @detailImageUrl, @seatselectURL);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand eventCmd = new SqlCommand(insertEventQuery, conn, transaction);
                    eventCmd.Parameters.AddWithValue("@eventName", txtEventName.Text);
                    eventCmd.Parameters.AddWithValue("@eventDateTime", eventDateTime);
                    eventCmd.Parameters.AddWithValue("@artistName", txtArtistName.Text);
                    eventCmd.Parameters.AddWithValue("@eventDetails", txtEventDetails.Text);
                    eventCmd.Parameters.AddWithValue("@eventStatus", ddlEventStatus.SelectedValue);
                    eventCmd.Parameters.AddWithValue("@imageUrl", (object)imageUrl ?? DBNull.Value);
                    eventCmd.Parameters.AddWithValue("@detailImageUrl", (object)detailImageUrl ?? DBNull.Value);
                    eventCmd.Parameters.AddWithValue("@seatselectURL", (object)seatSelectUrl ?? DBNull.Value);

                    int eventId = Convert.ToInt32(eventCmd.ExecuteScalar());
                    string formattedEventId = "E" + eventId.ToString("D3");

                    InsertSeatCategory(conn, transaction, ddlVenue.SelectedValue, formattedEventId, "VVIP", vvipQty, vvipPrice);
                    InsertSeatCategory(conn, transaction, ddlVenue.SelectedValue, formattedEventId, "VIP", vipQty, vipPrice);
                    InsertSeatCategory(conn, transaction, ddlVenue.SelectedValue, formattedEventId, "Standard", standardQty, standardPrice);

                    transaction.Commit();
                    resultMessage.InnerText = "Event and seat categories created successfully!";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    resultMessage.InnerText = "Error: " + ex.Message;
                }
            }
        }
        protected void cvEventDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime eventDate;
            if (DateTime.TryParse(args.Value, out eventDate))
            {
                if (eventDate < DateTime.Now)
                {
                    args.IsValid = false; // Invalid if the date is in the past
                }
                else
                {
                    args.IsValid = true;
                }
            }
            else
            {
                args.IsValid = false; // Invalid if the date format is incorrect
            }
        }

    }
}