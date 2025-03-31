<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CreateEvent.aspx.cs" Inherits="ConcertTicketing.CreateEvent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffMainContent" runat="server">
    <script type="text/javascript">
        function confirmSubmission() {
            return confirm("Are you sure you want to submit the event?");
        }
    </script>
    <!-- Add Event Form -->
    <h2>Add New Event</h2>
    <div>
        <label for="txtEventName">Event Name:</label><br />
        <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" /><br />
        <asp:RequiredFieldValidator ID="rfvEventName" runat="server" ControlToValidate="txtEventName"
            ErrorMessage="Event Name is required!" ForeColor="Red" Display="Dynamic" /><br /><br />

        <!-- Event Date & Time -->
        <label for="txtEventDateTime">Event Date and Time:</label><br />
        <asp:TextBox ID="txtEventDateTime" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvEventDateTime" runat="server" ControlToValidate="txtEventDateTime"
            ErrorMessage="Event Date & Time is required!" ForeColor="Red" CssClass="error" Display="Dynamic" /><br />
        <asp:CustomValidator ID="cvEventDate" runat="server" ControlToValidate="txtEventDateTime"
            ErrorMessage="Event Date cannot be in the past!" ForeColor="Red" CssClass="error" Display="Dynamic"
            OnServerValidate="cvEventDate_ServerValidate" ClientValidationFunction="validateDate"></asp:CustomValidator><br />

        <label for="txtArtistName">Artist Name:</label><br />
        <asp:TextBox ID="txtArtistName" runat="server" CssClass="form-control" /><br />
        <asp:RequiredFieldValidator ID="rfvArtistName" runat="server" ControlToValidate="txtArtistName"
            ErrorMessage="Artist Name is required!" ForeColor="Red" CssClass="error" Display="Dynamic" /><br />

        <label for="txtEventDetails">Event Details:</label><br />
        <asp:TextBox ID="txtEventDetails" runat="server" CssClass="form-control" TextMode="MultiLine" /><br />
        <asp:RequiredFieldValidator ID="rfvEventDetails" ForeColor="Red" runat="server" ControlToValidate="txtEventDetails"
            ErrorMessage="Event Details are required!"  CssClass="error" Display="Dynamic" /><br /><br />

</asp:Content>
