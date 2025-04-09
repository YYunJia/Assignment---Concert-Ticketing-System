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

     <div class="container">
     <!-- Left Container (Seat Summary) -->
     <div class="left-container" id="seatSummary">
         <div class="left-header">
             <h3>Seat Summary</h3>
         </div>
         <!-- Dynamic seat list will be inserted here -->
     </div>

     <!-- Middle Container (Seating Map) -->
     <div class="middle-container">
         <div class="image-map-container">
             <asp:Image ID="imgEventMap" runat="server" CssClass="image-map" usemap="#image-map" alt="Seating Map" />
             <map name="image-map" id="imageMapArea">
                 <!-- Dynamic areas will be inserted here -->
             </map>
         </div>
     </div>

     <!-- Right Container (Summary) -->
     <div class="right-container">
         <h3>Summary</h3>
         <div class="item-container">
             <h4><asp:Label ID="lblEventName" runat="server" Text=""></asp:Label></h4>
         </div>
         <div class="item-container">
             <img src="Image/location.png" alt="Location">
             <span><asp:Label ID="lblVenueLocation" runat="server" Text=""></asp:Label></span>
         </div>
         <div class="item-container">
             <img src="Image/calendarr.png" alt="Calendar">
             <span><asp:Label ID="lblEventDate" runat="server" Text=""></asp:Label></span>
         </div>
         <div class="total-quantity" id="totalQuantity">Quantity: 0</div>
         <div class="total-price" id="totalPrice">Total Price: RM0</div>
         <asp:HiddenField ID="hdnTotalPrice" runat="server" />
         <asp:HiddenField ID="hdnNumOfTickets" runat="server" />
         <asp:HiddenField ID="hdnSelectedSeats" runat="server" />
         <asp:Button ID="btnProceedToPayment" runat="server" CssClass="proceed-button" Text="Proceed to Payment" OnClick="btnProceedToPayment_Click" />
     </div>
 </div>

 <script>
     document.addEventListener("DOMContentLoaded", function () {
         const seatSummary = document.getElementById('seatSummary');
         const totalQuantity = document.getElementById('totalQuantity');
         const totalPrice = document.getElementById('totalPrice');
         const imageMapArea = document.getElementById('imageMapArea');
         let seatCounts = {
             standard: { count: 0, price: seatData.standard.price },
             vip: { count: 0, price: seatData.vip.price },
             vvip1: { count: 0, price: seatData.vvip1.price },
             vvip2: { count: 0, price: seatData.vvip2.price }
         };

         console.log("Seat Data:", seatData); // Debugging

         if (typeof seatData !== 'undefined') {
             const areasData = [
                 { coords: seatData.standard.coords, category: "standard", price: seatData.standard.price },
                 { coords: seatData.vip.coords, category: "vip", price: seatData.vip.price },
                 { coords: seatData.vvip1.coords, category: "vvip1", price: seatData.vvip1.price },
                 { coords: seatData.vvip2.coords, category: "vvip2", price: seatData.vvip2.price }
             ];

             areasData.forEach(area => {
                 if (area.coords) {
                     const areaTag = document.createElement('area');
                     areaTag.setAttribute('shape', 'rect');
                     areaTag.setAttribute('coords', area.coords);
                     areaTag.setAttribute('data-category', area.category);
                     areaTag.setAttribute('data-price', area.price);
                     areaTag.setAttribute('href', '#');
                     areaTag.addEventListener('click', function (event) {
                         event.preventDefault();
                         const category = areaTag.getAttribute('data-category');
                         console.log("Clicked Area:", category);
                         updateSeat(category, 1);
                     });
                     imageMapArea.appendChild(areaTag);
                 }
             });
         } else {
             console.error("seatData is undefined.");
         }

         function renderSeatSummary() {
             seatSummary.innerHTML = `
         <div class="left-header">
             <h3>Seat Summary</h3>
         </div>
     `;
             Object.keys(seatCounts).forEach(category => {
                 if (seatCounts[category].count > 0) {
                     const seatEntry = document.createElement('div');
                     seatEntry.classList.add('seat-entry');
                     seatEntry.innerHTML = `
                 <span>${category.toUpperCase()} x${seatCounts[category].count} (RM${seatCounts[category].price * seatCounts[category].count})</span>
                 <div>
                     <button onclick="updateSeat('${category}', -1)">-</button>
                     <button onclick="updateSeat('${category}', 1)">+</button>
                 </div>
             `;
                     seatSummary.appendChild(seatEntry);
                 }
             });
             updateTotalQuantityAndPrice();
         }

         function updateSeat(category, change) {
             seatCounts[category].count += change;
             if (seatCounts[category].count < 0) seatCounts[category].count = 0;
             renderSeatSummary();
         }

         function updateTotalQuantityAndPrice() {
             const totalQty = Object.values(seatCounts).reduce((sum, seat) => sum + seat.count, 0);
             const totalPrc = Object.values(seatCounts).reduce((sum, seat) => sum + seat.count * seat.price, 0);
             totalQuantity.textContent = `Quantity: ${totalQty}`;
             totalPrice.textContent = `Total Price: RM${totalPrc}`;
             document.getElementById('<%= hdnTotalPrice.ClientID %>').value = totalPrc;
     document.getElementById('<%= hdnNumOfTickets.ClientID %>').value = totalQty;

     // Update selected seats
     let selectedSeats = [];
     Object.keys(seatCounts).forEach(category => {
         if (seatCounts[category].count > 0) {
             selectedSeats.push(`${category}:${seatCounts[category].count}`);
         }
     });
             document.getElementById('<%= hdnSelectedSeats.ClientID %>').value = selectedSeats.join(',');
         }

         renderSeatSummary();
     });
 </script>
</asp:Content>
