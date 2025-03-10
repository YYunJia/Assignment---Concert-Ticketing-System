<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSite.aspx.cs" Inherits="ConcertTicketing.AdminSite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>StarConcert - Staff</title>
    <link rel="stylesheet" type="text/css" href="Design/MasterDeisgn.css" />
    <link rel="stylesheet" type="text/css" href="Design/Register.css" />
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

                <!-- Password (User Input) -->
                <div class="input-field">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" MaxLength="255"></asp:TextBox>
                </div>

                <!-- Confirm Password (User Input for Validation) -->
                <div class="input-field">
                    <label for="txtConfirmPassword">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" MaxLength="255"></asp:TextBox>
                    <asp:CompareValidator
                        ID="cvConfirmPassword"
                        runat="server"
                        ControlToValidate="txtConfirmPassword"
                        ControlToCompare="txtPassword"
                        ForeColor="Red"
                        ErrorMessage="Passwords do not match."
                        Display="Dynamic" />
                </div>

                <!-- Role (Hidden or predefined as 'Customer') -->
                <div class="input-field" style="display: none;">
                    <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" Text="Customer" ReadOnly="true"></asp:TextBox>
                </div>

                <!-- Position Dropdown List -->
                <div class="input-field">
                    <label for="ddlPosition">Position</label>
                    <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control" Required="true">
                        <asp:ListItem Text="Select Position" Value="" />
                        <asp:ListItem Text="Security" Value="Security" />
                        <asp:ListItem Text="Sound Technicians" Value="SoundTechnicians" />
                        <asp:ListItem Text="Lighting Technicians" Value="LightingTechnicians" />
                        <asp:ListItem Text="Hospitality" Value="Hospitality" />
                        <asp:ListItem Text="Box Office" Value="BoxOffice" />
                    </asp:DropDownList>
                </div>

                <!-- Register Button -->
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn" OnClick="btnRegister_Click" />

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
