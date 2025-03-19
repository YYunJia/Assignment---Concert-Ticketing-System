<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="WebAssignment.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: 'Playfair Display', serif;
            background-color: #f4f0e6;
            color: #4a4a4a;
        }

        .wrapper {
            display: flex;
            justify-content: space-between;
            width: 100%;
            padding: 30px;
            max-width: 1200px;
            margin: 0 auto;
        }

        .left-side {
            width: 60%;
            padding: 30px;
            background-color: #ffffff;
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .left-side img {
            width: 100%;
            height: auto;
            border-radius: 10px;
        }

        .shadow-line {
            width: 100%;
            height: 1px;
            background-color: #e0e0e0;
            margin: 25px 0;
        }

        .seat-details {
            width: 100%;
            padding: 25px;
            background-color: #f8f4ef;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
        }

        .seat-details h3 {
            font-size: 24px;
            margin-bottom: 20px;
            color: #333333;
            font-family: 'Cinzel', serif;
        }

        .seat-details div {
            font-size: 18px;
            font-family: 'Lora', serif;
            color: #555555;
        }

        .cart-container {
            width: 35%;
            padding: 30px;
            background-color: #ffffff;
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .cart-header {
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 25px;
            color: #333333;
            font-family: 'Cinzel', serif;
            text-align: center;
        }

        .cart-details {
            margin-bottom: 25px;
        }

        .cart-details div {
            margin-bottom: 15px;
            font-size: 18px;
            font-family: 'Lora', serif;
            color: #555555;
        }

        .cart-details span {
            font-weight: bold;
            color: #333333;
        }

        .checkout-button {
            display: block;
            width: 100%;
            padding: 15px;
            background-color: #8b7355;
            color: #ffffff;
            font-size: 20px;
            font-weight: bold;
            text-align: center;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            transition: background-color 0.3s;
            font-family: 'Cinzel', serif;
        }

        .checkout-button:hover {
            background-color: #6b5a45;
        }

        .timer {
            font-size: 20px;
            font-weight: bold;
            color: #8b7355;
            font-family: 'Cinzel', serif;
        }
    </style>
    <div class="wrapper">
        <div class="left-side">
            <asp:Image ID="imgEvent" runat="server" />
            <div class="shadow-line"></div>
            <div class="seat-details">
                <h3>Seat Allocation</h3>
                <asp:Label ID="lblSeatDetails" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="cart-container">
            <div class="cart-header">Cart</div>
            <div class="cart-details">
                <div>Time remaining: <span class="timer" id="timer">05:00</span></div>
                <div>Event: <asp:Label ID="lblEventName" runat="server" Text=""></asp:Label></div>
                <div>Subtotal: <asp:Label ID="lblSubtotal" runat="server" Text="RM0.00"></asp:Label></div>
                <div>Booking Fee (1%): <asp:Label ID="lblBookingFee" runat="server" Text="RM0.00"></asp:Label></div>
                <div>Operational Fee (3%): <asp:Label ID="lblOperationalFee" runat="server" Text="RM0.00"></asp:Label></div>
                <div>Total: <asp:Label ID="lblTotalPrice" runat="server" Text="RM0.00"></asp:Label></div>
            </div>
            <asp:Button ID="btnCheckOut" runat="server" CssClass="checkout-button" Text="Check Out" OnClick="btnCheckOut_Click" OnClientClick="stopTimer();" />
        </div>
    </div>

    <script>
        let timeLeft = 15; // 5 minutes = 300 seconds
        let timerInterval;

        // Function to update the timer display
        function updateTimer() {
            const minutes = Math.floor(timeLeft / 60);
            const seconds = timeLeft % 60;
            document.getElementById('timer').textContent = `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`;

            // If time runs out, redirect to MainPage.aspx
            if (timeLeft <= 0) {
                clearInterval(timerInterval); // Stop the timer
                window.location.href = "MainPage.aspx";
            } else {
                timeLeft--; // Decrement the timer
            }
        }

        // Start the timer when the page loads
        timerInterval = setInterval(updateTimer, 1000);

        // Stop the timer when the "Check Out" button is clicked
        function stopTimer() {
            clearInterval(timerInterval); // Stop the timer
        }
    </script>
</asp:Content>