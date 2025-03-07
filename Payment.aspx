<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="WebAssignment.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: 'Playfair Display', serif;
            background-color: #f4f0e6;
            color: #4a4a4a;
            margin: 0;
            padding: 0;
        }

        .payment-container {
            display: flex;
            justify-content: space-between;
            width: 90%;
            max-width: 1200px;
            margin: 30px auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .payment-methods {
            width: 60%;
            padding: 20px;
        }

        </style>
     <div class="payment-container">
       <!-- Payment Methods Section -->
       <div class="payment-methods">
           <h2>Choose Your Payment Method</h2>
           <div class="payment-options">
               <asp:ImageButton ID="btnVisa" runat="server" ImageUrl="/visa.png" CssClass="paymentOption" OnClick="btnVisa_Click" />
              
           </div>
        </div>
       </div>
</asp:Content>
