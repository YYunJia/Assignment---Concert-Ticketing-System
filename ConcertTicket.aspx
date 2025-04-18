﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="ConcertTicket.aspx.cs" Inherits="ConcertTicketing.ConcertTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <html>

    <head>
        <title>Event Card</title>
        <script type="text/javascript">
            function redirectToDetails(eventId) {
                window.location.href = "Details.aspx?eventId=" + eventId;
            }
        </script>
        <link rel="stylesheet" type="text/css" href="ConcertTicket.css" />
        <script src="script.js"></script>
    </head>
    <body>
        <br />
        <asp:PlaceHolder ID="EventContainer" runat="server"></asp:PlaceHolder>
    </body>

    </html>
</asp:Content>
