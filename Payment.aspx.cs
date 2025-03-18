using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Web.UI;

namespace WebAssignment
{
    public partial class Payment : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve data from session variables
                string eventName = Session["EventName"] as string;
                string subtotal = Session["Subtotal"] as string;
                string bookingFee = Session["BookingFee"] as string;
                string operationFee = Session["OperationFee"] as string;
                string totalAmount = Session["TotalAmount"] as string;
                string allocatedSeats = Session["AllocatedSeats"] as string;
                string eventId = Session["EventId"] as string;
                string venueId = Session["VenueId"] as string;

                // Debug: Check if eventId and venueId are retrieved correctly
                System.Diagnostics.Debug.WriteLine("EventId from Session: " + eventId);
                System.Diagnostics.Debug.WriteLine("VenueId from Session: " + venueId);

                if (string.IsNullOrEmpty(eventId) || string.IsNullOrEmpty(venueId))
                {
                    ShowErrorMessage("EventId or VenueId is missing.");
                    return;
                }

// Populate labels
                lblEventName.Text = eventName;
                lblSubtotal.Text = $"RM{subtotal}";
                lblBookingFee.Text = $"RM{bookingFee}";
                lblOperationalFee.Text = $"RM{operationFee}";
                lblTotal.Text = $"RM{totalAmount}";

                // Initialize panels
                pnlDebitCard.Visible = false;
                pnlCreditCard.Visible = false;
                pnlTNG.Visible = false;
                pnlAlipay.Visible = false;

                // Debug: Check if allocated seats, eventId, and venueId are retrieved correctly
                System.Diagnostics.Debug.WriteLine("Allocated Seats: " + allocatedSeats);
                System.Diagnostics.Debug.WriteLine("EventId: " + eventId);
                System.Diagnostics.Debug.WriteLine("VenueId: " + venueId);
            }
        }

        protected void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: Retrieve and validate session data
                string userId = Session["UserID"] as string;
                string eventId = Session["EventId"] as string;
                string venueId = Session["VenueId"] as string;
                string allocatedSeats = Session["AllocatedSeats"] as string;
                string totalAmount = Session["TotalAmount"] as string;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(eventId) || string.IsNullOrEmpty(venueId) ||
                    string.IsNullOrEmpty(allocatedSeats) || string.IsNullOrEmpty(totalAmount))
                {
                    ShowErrorMessage("Required session data is missing. Please try again.");
                    return;
                }

                // Step 2: Parse the allocated seats
                List<string> seatList = new List<string>();
                string[] seatCategories = allocatedSeats.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string seatCategory in seatCategories)
                {
                    string[] parts = seatCategory.Split(':');
                    string category = parts[0];
                    string[] seatNumbers = parts[1].Split(',');

                    foreach (string seatNo in seatNumbers)
                    {
                        seatList.Add($"{category}:{seatNo.Trim()}"); // Trim to remove any extra spaces
                    }
                }

                // Step 3: Calculate the number of tickets based on allocated seats
                int numOfTickets = seatList.Count;

                if (numOfTickets == 0)
                {
                    ShowErrorMessage("No seats allocated. Please try again.");
                    return;
                }

                // Step 4: Fetch customerId using userId
                string customerId = GetCustomerIdByUserId(userId);

                if (string.IsNullOrEmpty(customerId))
                {
                    ShowErrorMessage("Customer ID not found.");
                    return;
                }

                // Step 5: Insert payment details
                string transactionNumber = GenerateTransactionNumber();
                string paymentMethod = GetSelectedPaymentMethod(); // Implement this method based on your UI
                string paymentStatus = "Completed"; // Default status

                if (string.IsNullOrEmpty(paymentMethod))
                {
                    ShowErrorMessage("Payment method is not selected.");
                    return;
                }

                string paymentId = InsertPayment(transactionNumber, totalAmount, paymentMethod, paymentStatus);

                if (string.IsNullOrEmpty(paymentId))
                {
                    ShowErrorMessage("Failed to process payment.");
                    return;
                }

                // Step 6: Save booking details to the database
                SaveBooking(totalAmount, numOfTickets, customerId, paymentId, eventId);

                // Step 7: Retrieve the bookingId of the newly created booking
                string bookingId = GetLatestBookingId(customerId, eventId);

                if (string.IsNullOrEmpty(bookingId))
                {
                    ShowErrorMessage("Failed to retrieve booking ID.");
                    return;
                }

                // Step 8: Insert tickets for each allocated seat
                List<string> ticketIds = new List<string>();
                foreach (string seat in seatList)
                {
                    string[] parts = seat.Split(':');
                    string category = parts[0];
                    string seatNo = parts[1];

                    string ticketNo = GenerateTicketNumber(); // Generate a unique ticket number
                    string ticketId = InsertTicket(ticketNo, bookingId, eventId, seatNo); // Insert ticket and get the generated ticketId
                    if (!string.IsNullOrEmpty(ticketId))
                    {
                        ticketIds.Add(ticketId); // Store the ticketId for later use
                    }
                }

                // Step 9: Store the ticketIds in the session for display
                Session["TicketIds"] = ticketIds;

                // Step 10: Update seat status (if applicable)
                if (!UpdateSeatStatus(seatList))
                {
                    ShowErrorMessage("Some seats could not be updated. Please check the seat availability.");
                    return;
                }

                // Step 11: Store the bookingId in the session for the confirmation page
                Session["BookingId"] = bookingId;

                // Step 12: Redirect to confirmation page
                Response.Redirect("confirmation.aspx");
            }
            catch (ThreadAbortException)
            {
                // Ignore ThreadAbortException (caused by Response.Redirect)
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while processing your payment.");
                System.Diagnostics.Debug.WriteLine($"Payment Error: {ex.ToString()}");
            }
        }

        private string GetCustomerIdByUserId(string userId)
        {
            string customerId = "";
            string query = "SELECT customerId FROM Customer WHERE userId = @userId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        customerId = result.ToString();
                    }
                }
            }

            return customerId;
        }

        private string InsertPayment(string transactionNumber, string totalAmount, string paymentMethod, string paymentStatus)
        {
            try
            {
                string query = @"
                    INSERT INTO Payment (paymentDateTime, transactionNumber, totalAmount, paymentMethod, paymentStatus)
                    OUTPUT INSERTED.paymentId
                    VALUES (@paymentDateTime, @transactionNumber, @totalAmount, @paymentMethod, @paymentStatus)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@paymentDateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@transactionNumber", transactionNumber);
                        cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                        cmd.Parameters.AddWithValue("@paymentStatus", paymentStatus);

                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString(); // Return the generated paymentId
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while processing the payment.");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return null;
        }

        private void SaveBooking(string totalPrice, int numOfTickets, string customerId, string paymentId, string eventId)
        {
            try
            {
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
                ShowErrorMessage("An error occurred while saving the booking.");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private string GetLatestBookingId(string customerId, string eventId)
        {
            string bookingId = "";
            string query = @"
                SELECT TOP 1 bookingId
                FROM Booking
                WHERE customerId = @customerId AND eventId = @eventId
                ORDER BY bookingDateTime DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        bookingId = result.ToString();
                    }
                }
            }

            return bookingId;
        }

        private string InsertTicket(string ticketNo, string bookingId, string eventId, string seatNo)
        {
            try
            {
                string query = @"
            INSERT INTO Ticket (ticketNo, bookingId, eventId, seatNo)
            OUTPUT INSERTED.ticketId
            VALUES (@ticketNo, @bookingId, @eventId, @seatNo)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ticketNo", ticketNo);
                        cmd.Parameters.AddWithValue("@bookingId", bookingId);
                        cmd.Parameters.AddWithValue("@eventId", eventId);
                        cmd.Parameters.AddWithValue("@seatNo", seatNo);

                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString(); // Return the generated ticketId
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while inserting the ticket.");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return null;
        }

        private bool UpdateSeatStatus(List<string> seatList)
        {
            try
            {
                string eventId = Session["EventId"] as string;
                string venueId = Session["VenueId"] as string;

                if (string.IsNullOrEmpty(eventId) || string.IsNullOrEmpty(venueId))
                {
                    ShowErrorMessage("Event ID or Venue ID is missing.");
                    return false;
                }

                bool allSeatsUpdated = true;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (string seat in seatList)
                    {
                        string[] parts = seat.Split(':');
                        string category = parts[0];
                        string seatNo = parts[1];

                        string query = @"
                    UPDATE Seat
                    SET status = 'Booked'
                    WHERE seatNo = @seatNo AND eventId = @eventId AND venueId = @venueId AND status = 'Available'";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@seatNo", seatNo);
                            cmd.Parameters.AddWithValue("@eventId", eventId);
                            cmd.Parameters.AddWithValue("@venueId", venueId);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                // Log the error but continue processing other seats
                                System.Diagnostics.Debug.WriteLine($"Seat {seatNo} is already booked or does not exist.");
                                allSeatsUpdated = false;
                            }
                        }
                    }
                }

                return allSeatsUpdated;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("An error occurred while updating seat status.");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        private string GenerateTransactionNumber()
        {
            // Example: Generate a unique transaction number based on timestamp
            return "TXN-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private string GenerateTicketNumber()
        {
            // Example: Generate a unique ticket number based on timestamp
            return "TICKET-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private string GetSelectedPaymentMethod()
        {
            // Implement logic to retrieve the selected payment method from the UI
            if (pnlDebitCard.Visible) return "Debit Card";
            if (pnlCreditCard.Visible) return "Credit Card";
            if (pnlTNG.Visible) return "Touch 'n Go";
            if (pnlAlipay.Visible) return "Alipay";
            return "Unknown";
        }

        protected void btnVisa_Click(object sender, ImageClickEventArgs e)
        {
            pnlDebitCard.Visible = true;
            pnlCreditCard.Visible = false;
            pnlTNG.Visible = false;
            pnlAlipay.Visible = false;
        }

        protected void btnMasterCard_Click(object sender, ImageClickEventArgs e)
        {
            pnlDebitCard.Visible = false;
            pnlCreditCard.Visible = true;
            pnlTNG.Visible = false;
            pnlAlipay.Visible = false;
        }

        protected void btnAlipay_Click(object sender, ImageClickEventArgs e)
        {
            pnlDebitCard.Visible = false;
            pnlCreditCard.Visible = false;
            pnlTNG.Visible = false;
            pnlAlipay.Visible = true;
        }

        protected void btnTNGO_Click(object sender, ImageClickEventArgs e)
        {
            pnlDebitCard.Visible = false;
            pnlCreditCard.Visible = false;
            pnlTNG.Visible = true;
            pnlAlipay.Visible = false;
        }

        private void ShowErrorMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showError", $"alert('{message}');", true);
        }
    }
}