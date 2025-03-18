<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="ConcertTicketing.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Details.css" />
     <div class="container">
     <!-- Left Content (70%) -->
     <div class="left-content" runat="server" id="leftContent">
         <div id="eventDetailsContainer" runat="server"></div>
     </div>

     <!-- Right Content (30%) -->
     <div class="right-content">
         <div class="ticket-pricing">
             <h2 style="font-size: 2em; color: #333; margin-bottom: 20px;">Ticket Pricing</h2>
             <div id="pricingContainer" runat="server"></div>
             <asp:Button ID="btnBuyTicket" runat="server" CssClass="buy-ticket-btn" Text="Buy Ticket" OnClick="btnBuyTicket_Click" />
         </div>
     </div>
 </div>
</asp:Content>
