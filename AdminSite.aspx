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
            <div class="navigation">
                <button class="category-button" id="btnRegisterStaff" runat="server" onserverclick="NavigateToRegisterStaff">Register Staff</button>
            </div>
            <div class="register-container">

                <!-- Title and Back Button Container -->
                <div class="title-container">
                    <!-- Back to Dashboard Image Button -->
                    <asp:ImageButton
                        ID="imgBackToDashboard"
                        runat="server"
                        ImageUrl="~/Images/back.png"
                        OnClick="btnBackToDashboard_Click"
                        CssClass="back-button"
                        CausesValidation="false"
                        formnovalidate="formnovalidate"
                        AlternateText="Back to Dashboard" />

                    <!-- Title -->
                    <h2>Register New Staff</h2>
                </div>

                <!-- Name (User Input) -->
                <div class="input-field">
                    <label for="txtName">Full Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Required="true" MaxLength="100"></asp:TextBox>
                </div>
                <!-- Email (User Input) -->
                <div class="input-field">
                    <label for="txtEmail">Email Address</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Required="true" MaxLength="100" TextMode="Email"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="regEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                        ForeColor="Red"
                        ErrorMessage="Please enter a valid email address."
                        Display="Dynamic" />
                </div>

                <!-- Contact (User Input) -->
                <div class="input-field">
                    <label for="txtContact">Contact Number</label>
                    <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="regContact"
                        runat="server"
                        ControlToValidate="txtContact"
                        ValidationExpression="^\d{3}-?\d{7,10}$"
                        ForeColor="Red"
                        ErrorMessage="Please enter in the format (010-1234567)."
                        Display="Dynamic" />
                </div>

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
