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

        .left-header {
            background-color: #333;
            color: #fff;
            padding: 10px;
            text-align: center;
            border-radius: 5px;
            margin-bottom: 15px;
        }

        .seat-entry {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 10px;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

            .seat-entry span {
                font-size: 14px;
            }

            .seat-entry button {
                width: 30px;
                height: 30px;
                font-size: 16px;
                font-weight: bold;
                color: white;
                border: none;
                border-radius: 5px;
                cursor: pointer;
                transition: background-color 0.3s;
            }

                .seat-entry button:nth-child(1) {
                    background-color: red;
                }

                    .seat-entry button:nth-child(1):hover {
                        background-color: darkred;
                    }

                .seat-entry button:nth-child(2) {
                    background-color: green;
                }

                    .seat-entry button:nth-child(2):hover {
                        background-color: darkgreen;
                    }

        .item-container {
            margin-bottom: 20px;
        }

            .item-container img {
                max-width: 50px;
                margin-right: 10px;
                vertical-align: middle;
            }

        .proceed-button {
            display: block;
            width: 100%;
            padding: 10px;
            background-color: pink;
            color: black;
            font-size: 16px;
            font-weight: bold;
            text-align: center;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

            .proceed-button:hover {
                background-color: hotpink;
            }

        .total-quantity {
            margin: 20px 0;
            text-align: center;
            font-size: 16px;
            font-weight: bold;
            color: #333;
        }

        .total-price {
            margin: 20px 0;
            text-align: center;
            font-size: 16px;
            font-weight: bold;
            color: #333;
        }
    </style>
</asp:Content>
