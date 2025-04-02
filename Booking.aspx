<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="ConcertTicketing.Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .container {
            display: flex;
            width: 100%;
            gap: 20px;
            padding: 20px;
            box-sizing: border-box;
        }

        .left-container {
            width: 25%;
            background-color: #f4f4f4;
            padding: 10px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .middle-container {
            width: 45%;
            background-color: #ffffff;
            padding: 10px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .right-container {
            width: 30%;
            background-color: #eaeaea;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .image-map-container {
            position: relative;
            max-width: 100%;
            height: auto;
        }

        .image-map {
            max-width: 100%;
            height: auto;
            border-radius: 10px;
        }
    </style>
</asp:Content>
