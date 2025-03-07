<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ConcertTicketing.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: 'Playfair Display', serif;
            background-color: #f4f0e6;
            color: #4a4a4a;
            margin: 0;
            padding: 0;
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
