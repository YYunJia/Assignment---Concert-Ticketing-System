<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" 
    CodeBehind="LoginPage.aspx.cs" Inherits="ConcertTicketing.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container">
        <!-- Title -->
        <h2>LOG IN</h2>

        <!-- Email Field -->
        <div class="input-field">
            <label for="txtEmail">Email Address</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            
        </div>

        <!-- Password Field -->
        <div class="input-field">
            <label for="txtPwd">Password</label>
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="form-control" />
        </div>

        <!-- Forgot Password Link -->
        <asp:LinkButton ID="btnForgotPwd" runat="server" CssClass="forgot-password">Forgot Password?</asp:LinkButton>

        <!-- Login Button -->
        <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="btn" OnClick="btnLogin_Click" />


        <!-- Register Link -->
        <div class="footer-text">
            Don't have an account?
            <asp:LinkButton ID="btnRegister" runat="server" CssClass="footer-link" PostBackUrl="~/Register.aspx">Register</asp:LinkButton>
        </div>

    </div>
</asp:Content>
