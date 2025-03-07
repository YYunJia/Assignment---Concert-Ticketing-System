<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSite.aspx.cs" Inherits="ConcertTicketing.AdminSite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>StarConcert - Staff</title>
    <link rel="stylesheet" type="text/css" href="Design/MasterDeisgn.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <div>
                <img src="Images/logo.png" alt="StarConcert Logo" />
            </div>
            <div class="right-section">
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search.png" Width="30px" />

                <asp:LinkButton ID="btnLoginRegister" runat="server" PostBackUrl="~/LoginPage.aspx"
                    Style="color: black; font-size: 18px; font-weight: bold; margin-right: 15px;">
                     <span style="vertical-align: middle;">Login/Register</span>
                </asp:LinkButton>

                <!-- Profile Image Button -->
                <asp:ImageButton ID="btnProfile" runat="server" ImageUrl="~/Images/profile.png" Width="30px"
                    Style="vertical-align: middle;" />
            </div>
        </div>

        <!-- Content Section -->
        <div class="content">

            <!-- Navigation Bar -->
            <nav style="background-color: #f4f4f4; padding: 10px; margin-bottom: 20px; border-radius: 5px;">
                <ul style="list-style-type: none; padding: 0; display: flex; gap: 20px;">
                    <li>
                        <a href="MainPageAdmin.aspx" style="text-decoration: none; color: #007BFF; font-weight: bold;">Add</a>
                    </li>
                    <li>
                        <a href="MainPageAdminModify.aspx" style="text-decoration: none; color: #007BFF; font-weight: bold;">Modify</a>
                    </li>
                    <li>
                        <a href="MainPageAdminRemove.aspx" style="text-decoration: none; color: #007BFF; font-weight: bold;">Remove</a>
                    </li>
                </ul>
            </nav>

        </div>

        <!-- Footer Section -->
        <footer>
            <div style="padding: 20px 0; background-color: #1c1a1b; color: #f5f5f5; text-align: center;">
                <!-- Footer Links -->
                <p>
                    <a href="/FAQ" style="color: #f5f5f5; text-decoration: none;">FAQ</a> | 
                    <a href="/aboutUs" style="color: #f5f5f5; text-decoration: none;">About Us</a> | 
                    <a href="/terms" style="color: #f5f5f5; text-decoration: none;">Terms and Conditions</a> | 
                    <a href="/privacy" style="color: #f5f5f5; text-decoration: none;">Privacy Policy</a>
                </p>

                <!-- Social Media Links -->
                <p>
                    Follow Us: 
                    <a href="https://www.instagram.com" target="_blank" style="color: #f5f5f5; text-decoration: none;">
                        <img src="Images/facebook.png" alt="Facebook" class="social-icon" />
                    </a>
                    <a href="https://www.facebook.com" target="_blank" style="color: #f5f5f5; text-decoration: none;">
                        <img src="Images/instagram.png" alt="Instagram" class="social-icon" />
                    </a>
                </p>

                <!-- Copyright Notice -->
                <p style="font-size: 12px; color: #C0C0C0; font-style: italic;">
                    Copyright © StarConcert SDN BHD. All Rights Reserved.
                </p>
            </div>
        </footer>
    </form>
</body>
</html>
