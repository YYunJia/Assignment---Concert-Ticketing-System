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

         .payment-methods h2 {
     font-size: 28px;
     font-weight: bold;
     margin-bottom: 25px;
     color: #333333;
     font-family: 'Cinzel', serif;
     text-align: center;
 }

 .payment-options {
     display: flex;
     justify-content: space-around;
     margin-bottom: 25px;
     gap: 10px; /* Add spacing between images */
 }

 .payment-options .paymentOption {
     width: 80px; /* Smaller size for images */
     height: 80px; /* Smaller size for images */
     border: 1px solid #ccc;
     border-radius: 10px;
     padding: 5px; /* Reduced padding */
     background-color: #f8f4ef;
     transition: transform 0.3s;
 }

 .payment-options .paymentOption:hover {
     transform: scale(1.1);
 }

 .payment-details {
     margin-top: 25px;
 }

 .payment-details label {
     font-size: 18px;
     font-family: 'Lora', serif;
     color: #555555;
     display: block;
     margin-bottom: 5px;
 }

 .payment-details input {
     width: 100%;
     padding: 10px;
     margin-bottom: 15px;
     border: 1px solid #ccc;
     border-radius: 8px;
     font-size: 16px;
 }

 .cart-summary {
     width: 35%;
     padding: 20px;
     background-color: #f8f4ef;
     border-radius: 15px;
     box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
 }

 .cart-summary h3 {
     font-size: 24px;
     font-weight: bold;
     margin-bottom: 25px;
     color: #333333;
     font-family: 'Cinzel', serif;
     text-align: center;
 }

 .cart-summary div {
     font-size: 18px;
     font-family: 'Lora', serif;
     color: #555555;
     margin-bottom: 15px;
 }

 .cart-summary span {
     font-weight: bold;
     color: #333333;
 }

 .payment-buttons {
     display: flex;
     justify-content: space-between;
     margin-top: 25px;
 }

 .payment-buttons button {
     padding: 15px 30px;
     background-color: #8b7355;
     color: #ffffff;
     font-size: 18px;
     font-weight: bold;
     text-align: center;
     border: none;
     border-radius: 8px;
     cursor: pointer;
     transition: background-color 0.3s;
     font-family: 'Cinzel', serif;
 }

 .payment-buttons button:hover {
     background-color: #6b5a45;
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
