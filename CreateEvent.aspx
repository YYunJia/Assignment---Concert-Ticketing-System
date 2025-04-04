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

           <label for="ddlEventStatus">Event Status:</label><br />
        <asp:DropDownList ID="ddlEventStatus" runat="server"  CssClass="aspDropDownList">
            <asp:ListItem Text="-- Select Status --" Value="scheduled"></asp:ListItem>
            <asp:ListItem Text="Upcoming" Value="scheduled"></asp:ListItem>
            <asp:ListItem Text="Ongoing" Value="cancelled"></asp:ListItem>
        </asp:DropDownList><br />
        <asp:RequiredFieldValidator ID="rfvEventStatus" runat="server" ControlToValidate="ddlEventStatus"
            InitialValue="0" ErrorMessage="Please select an event status!" ForeColor="Red" CssClass="error" Display="Dynamic" /><br /><br />


        <label for="ddlVenue">Select Venue:</label><br />
        <asp:DropDownList ID="ddlVenue" runat="server" DataTextField="venueName" DataValueField="venueId" AutoPostBack="true"  CssClass="aspDropDownList">
            <asp:ListItem Text="Select Venue" Value=""></asp:ListItem>
        </asp:DropDownList><br />
        <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlVenue"
            InitialValue="0" ErrorMessage="Please select a venue!" ForeColor="Red" CssClass="error" Display="Dynamic" /><br />

        <!-- New Image Upload Field -->
        <asp:Label ID="lblUploadImage" runat="server" Text="Upload Background Image:"></asp:Label>
        <br /><br />
        <asp:FileUpload ID="fileUploadImage" runat="server" CssClass="aspFileUpload"/><br />

        <!-- New Detail Image Upload Field -->
        <asp:Label ID="lblUploadDetailImage" runat="server" Text="Upload Detail Image:"></asp:Label><br /><br />
        <asp:FileUpload ID="fileUploadDetailImage" runat="server" CssClass="aspFileUpload"/><br />

          <!-- Seat Category Fields -->
        <h3>Seat Categories</h3>
        <label for="txtVVIPQuantity">VVIP Quantity:</label><br />
        <asp:TextBox ID="txtVVIPQuantity" runat="server" CssClass="form-control" /><br />
        <asp:RequiredFieldValidator ID="rfvVVIPQuantity" runat="server" ControlToValidate="txtVVIPQuantity"
            ErrorMessage="VVIP Quantity is required!" ForeColor="Red" Display="Dynamic" /><br />
        <asp:RegularExpressionValidator ID="revVVIPQuantity" runat="server" ControlToValidate="txtVVIPQuantity"
            ValidationExpression="^\d+$" ErrorMessage="Must be a number!" ForeColor="Red" Display="Dynamic" /><br />

        <label for="txtVVIPPrice">VVIP Price:</label><br />
        <asp:TextBox ID="txtVVIPPrice" runat="server" CssClass="form-control" /><br />
        <asp:RequiredFieldValidator ID="rfvVVIPPrice" runat="server" ControlToValidate="txtVVIPPrice"
            ErrorMessage="VVIP Price is required!" ForeColor="Red" Display="Dynamic" /><br />
        <asp:RegularExpressionValidator ID="revVVIPPrice" runat="server" ControlToValidate="txtVVIPPrice"
            ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Must be a valid price!" ForeColor="Red" Display="Dynamic" /><br />

       
        <label for="txtVIPQuantity">VIP Quantity:</label><br />
        <asp:TextBox ID="txtVIPQuantity" runat="server"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvVIPQuantity" runat="server" ControlToValidate="txtVIPQuantity"
            ErrorMessage="VIP Quantity is required!" ForeColor="Red" Display="Dynamic" /><br />
        <asp:RegularExpressionValidator ID="revVIPQuantity" runat="server" ControlToValidate="txtVIPQuantity"
            ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Must be a valid price!" ForeColor="Red" Display="Dynamic" /><br />

        <label for="txtVIPPrice">VIP Price:</label><br />
        <asp:TextBox ID="txtVIPPrice" runat="server"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvVIPPrice" runat="server" ControlToValidate="txtVIPPrice"
            ErrorMessage="VIP Price is required!" ForeColor="Red" Display="Dynamic" /><br />
        <asp:RegularExpressionValidator ID="revVIPPrice" runat="server" ControlToValidate="txtVIPPrice"
            ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Must be a valid price!" ForeColor="Red" Display="Dynamic" /><br />

        <label for="txtStandardQuantity">Standard Quantity:</label><br />
        <asp:TextBox ID="txtStandardQuantity" runat="server"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvStandardQuantity" runat="server" ControlToValidate="txtStandardQuantity"
            ErrorMessage="Standard Quantity is required!" ForeColor="Red" Display="Dynamic" /><br />
        <asp:RegularExpressionValidator ID="revStandardQuantity" runat="server" ControlToValidate="txtStandardQuantity"
            ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Must be a valid price!" ForeColor="Red" Display="Dynamic" /><br />

        <label for="txtStandardPrice">Standard Price:</label><br />
        <asp:TextBox ID="txtStandardPrice" runat="server"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvStandardPrice" runat="server" ControlToValidate="txtStandardPrice"
            ErrorMessage="VVIP Price is required!" ForeColor="Red" Display="Dynamic" /><br />
        <asp:RegularExpressionValidator ID="revStandardPrice" runat="server" ControlToValidate="txtStandardPrice"
            ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Must be a valid price!" ForeColor="Red" Display="Dynamic" /><br />

          <!-- Seat Select Image Upload Field -->
        <asp:Label ID="lblUploadSeatSelectImage" runat="server" Text="Upload Seat Map Image:"></asp:Label><br /><br />
        <asp:FileUpload ID="fileUploadSeatSelectImage" runat="server" CssClass="aspFileUpload"/><br />


        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
            OnClientClick="return confirmSubmission();"
            OnClick="btnSubmit_Click" />
    </div>

    <!-- Display messages -->
    <div id="resultMessage" runat="server" style="color: green; margin-top: 20px;"></div>

</asp:Content>
