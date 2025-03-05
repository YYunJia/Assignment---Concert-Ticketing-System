<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="ConcertTicket.aspx.cs" Inherits="ConcertTicketing.ConcertTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="ConcertTicket.css" />

        <ItemTemplate>
            <div class="concert-card">
                <div class="concert-date">
                    <asp:Label ID="lblDay" runat="server" Text="SUN" CssClass="day" />
                    <asp:Label ID="lblMonthYear" runat="server" Text="20/Mar/2025" CssClass="month-year" />
                    <asp:Label ID="lblTime" runat="server" Text="20:00" CssClass="time" />
                </div>
                <div class="concert-image">
                    <asp:Image ID="imgConcert" runat="server" src="" CssClass="concert-img" />
                </div>
                <div class="concert-details">
                    <asp:Label ID="lblTitle" runat="server" Text="Beautiful As Always" CssClass="title" />
                    <asp:Label ID="lblArtist" runat="server" Text="Artist: Beyonce" CssClass="artist" />
                    <asp:Label ID="lblVenue" runat="server" Text="Star Genting | Genting Highland" CssClass="venue" />
                </div>
                
            </div>
        </ItemTemplate>

</asp:Content>
