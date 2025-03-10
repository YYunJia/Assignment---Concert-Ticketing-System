<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="ConcertTicket.aspx.cs" Inherits="ConcertTicketing.ConcertTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" type="text/css" href="ConcertTicket.css" />

    <itemtemplate>
        <div class="concert-card">
            <div class="concert-date">
                <asp:Label ID="lblDay" runat="server" Text="SUN" CssClass="day" />
                <asp:Label ID="lblMonthYear" runat="server" Text="20/Mar/2025" CssClass="month-year" />
                <asp:Label ID="lblTime" runat="server" Text="20:00" CssClass="time" />
            </div>
            <div class="concert-image">
                <asp:Image ID="imgConcert" runat="server" src="Images/beyonce(h).png" onclick="openModal(this.src)" CssClass="concert-img" />
            </div>
            <div class="concert-details">
                <asp:Label ID="lblTitle" runat="server" Text="Beautiful As Always" CssClass="title" />
                <asp:Label ID="lblArtist" runat="server" Text="Artist: Beyonce" CssClass="artist" />
                <asp:Label ID="lblVenue" runat="server" Text="Natianal Stadium | Bukit Jalil, Malaysia" CssClass="venue" />
            </div>
            <div class="concert-button">
                <asp:Button ID="btnFindTickets" runat="server" CssClass="btn-find-tickets" Text="View Concert" CommandArgument='<%# Eval("ConcertID") %>' />
            </div>
        </div>
    </itemtemplate>

    <itemtemplate>
        <div class="concert-card">
            <div class="concert-date">
                <asp:Label ID="Label7" runat="server" Text="SAT" CssClass="day" />
                <asp:Label ID="Label8" runat="server" Text="10/Apr/2025" CssClass="month-year" />
                <asp:Label ID="Label9" runat="server" Text="20:00" CssClass="time" />
            </div>
            <div class="concert-image">
                <asp:Image ID="Image2" runat="server" src="Images/THE WEEKND (v).png" onclick="openModal(this.src)" CssClass="concert-img" />
            </div>
            <div class="concert-details">
                <asp:Label ID="Label10" runat="server" Text="Crazy for Life" CssClass="title" />
                <asp:Label ID="Label11" runat="server" Text="Artist: The Weeknd" CssClass="artist" />
                <asp:Label ID="Label12" runat="server" Text="Axiata Arena | Genting Highland, Malaysia" CssClass="venue" />
            </div>
            <div class="concert-button">
                <asp:Button ID="Button2" runat="server" CssClass="btn-find-tickets" Text="View Concert" CommandArgument='<%# Eval("ConcertID") %>' />
            </div>
        </div>
    </itemtemplate>

    <itemtemplate>
        <div class="concert-card">
            <div class="concert-date">
                <asp:Label ID="Label1" runat="server" Text="SUN" CssClass="day" />
                <asp:Label ID="Label2" runat="server" Text="22/Feb/2025" CssClass="month-year" />
                <asp:Label ID="Label3" runat="server" Text="20:00" CssClass="time" />
            </div>
            <div class="concert-image">
                <asp:Image ID="Image1" runat="server" src="Images/km poster.png" onclick="openModal(this.src)" CssClass="concert-img" />
            </div>
            <div class="concert-details">
                <asp:Label ID="Label4" runat="server" Text="Golden hour" CssClass="title" />
                <asp:Label ID="Label5" runat="server" Text="Artist: Kacey" CssClass="artist" />
                <asp:Label ID="Label6" runat="server" Text="Indoor Stadium | Singapore" CssClass="venue" />
            </div>
            <div class="concert-button">
                <asp:Button ID="Button1" runat="server" CssClass="btn-find-tickets" Text="View Concert" CommandArgument='<%# Eval("ConcertID") %>' />
            </div>
        </div>
    </itemtemplate>

    <itemtemplate>
        <div class="concert-card">
            <div class="concert-date">
                <asp:Label ID="Label13" runat="server" Text="SUN" CssClass="day" />
                <asp:Label ID="Label14" runat="server" Text="10/Dec/2024" CssClass="month-year" />
                <asp:Label ID="Label15" runat="server" Text="20:00" CssClass="time" />
            </div>
            <div class="concert-image">
                <asp:Image ID="Image3" runat="server" src="Images/taylor(ver).png" onclick="openModal(this.src)" CssClass="concert-img" />
            </div>
            <div class="concert-details">
                <asp:Label ID="Label16" runat="server" Text="A Night To Remember" CssClass="title" />
                <asp:Label ID="Label17" runat="server" Text="Artist: Taylor Swift" CssClass="artist" />
                <asp:Label ID="Label18" runat="server" Text="Star Genting | Genting Highland | Malaysia" CssClass="venue" />
            </div>
            <div class="concert-button">
                <asp:Button ID="Button3" runat="server" CssClass="btn-find-tickets" Text="View Concert" CommandArgument='<%# Eval("ConcertID") %>' />
            </div>
        </div>
    </itemtemplate>

    <itemtemplate>
        <div class="concert-card">
            <div class="concert-date">
                <asp:Label ID="Label19" runat="server" Text="SUN" CssClass="day" />
                <asp:Label ID="Label20" runat="server" Text="20/Jan/2025" CssClass="month-year" />
                <asp:Label ID="Label21" runat="server" Text="20:00" CssClass="time" />
            </div>
            <div class="concert-image">
                <asp:Image ID="Image4" runat="server" src="Images/imagine dragon(h).png" onclick="openModal(this.src)" CssClass="concert-img" />
            </div>
            <div class="concert-details">
                <asp:Label ID="Label22" runat="server" Text="Imagination" CssClass="title" />
                <asp:Label ID="Label23" runat="server" Text="Artist: Imagine Dragon" CssClass="artist" />
                <asp:Label ID="Label24" runat="server" Text="Natianal Stadium | Bukit Jalil, Malaysia" CssClass="venue" />
            </div>
            <div class="concert-button">
                <asp:Button ID="Button4" runat="server" CssClass="btn-find-tickets" Text="View Concert" CommandArgument='<%# Eval("ConcertID") %>' />
            </div>
        </div>
    </itemtemplate>

    <div id="imageModal" class="modal">
        <span class="close" onclick="closeModal()">&times;</span>
        <img class="modal-content" id="fullImage">
    </div>

    <script>
        function openModal(imageSrc) {
            document.getElementById("imageModal").style.display = "flex";
            document.getElementById("fullImage").src = imageSrc;
        }

        function closeModal() {
            document.getElementById("imageModal").style.display = "none";
        }
    </script>



</asp:Content>
