<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="confirmation.aspx.cs" Inherits="WebAssignment.confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="confirmationContainer">
        <div class="confirmHeader">
            <p class="custName">Dear &nbsp<asp:Label ID="lblCust" runat="server" Text=""></asp:Label></p>
            <br />
            <p>Your booking is confirmed. Your E-ticket(s) are available for viewing below.</p>
        </div>
        <div class="viewTicket">
            <asp:Button ID="btnView" runat="server" Text="View E-Ticket" CssClass="btnView" PostBackUrl="~/ticket.aspx" />
        </div>
        <div class="booking-details">
            <p class="infoText">
                Please download your e-ticket(s). Each e-ticket is attached with a UNIQUE QR code (one-time use only) and admits one (01) person only.
            </p>

            <div class="reminderSection">
                <asp:Label ID="lblInfo" runat="server" Text="Important Reminders" CssClass="lblInfo"></asp:Label>
                <ul>
                    <li>Arrive at least 30 minutes before the event starts.</li>
                    <li>Bring a valid ID for ticket verification.</li>
                    <li>Ensure your QR code is accessible on your phone or printed.</li>
                    <li>Adhere to all event safety guidelines and policies.</li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>