<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" 
    CodeBehind="LoginPage.aspx.cs" Inherits="WebAssignment.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container">
        <!-- Title -->
        <h2>LOG IN</h2>

        <!-- Email Field -->
        <div class="input-field">
            <label for="txtEmail">Email Address <span style="color: red;">*</span></label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            <asp:RegularExpressionValidator
                ID="regEmail"
                runat="server"
                ControlToValidate="txtEmail"
                ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                ForeColor="Red"
                ErrorMessage="Please enter a valid email address."
                Display="Dynamic" />
            
        </div>

        <!-- Password Field -->
        <div class="input-field">
            <label for="txtPwd">Password <span style="color: red;">*</span></label>
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="form-control" />
            
            <asp:RegularExpressionValidator
                ID="regPasswordStrength"
                runat="server"
                ControlToValidate="txtPwd"
                ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
                ForeColor="Red"
                ErrorMessage="Password must be at least 8 characters, include an uppercase letter, a number, and a special character."
                Display="Dynamic" />
        </div>

        <!-- Forgot Password Link -->
        <asp:LinkButton ID="btnForgotPwd" runat="server" CssClass="forgot-password" PostBackUrl="~/ForgotPwd.aspx">Forgot Password?</asp:LinkButton>

        <!-- Login Button -->
        <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="btn" OnClick="btnLogin_Click" />


        <!-- Register Link -->
        <div class="footer-text">
            Don't have an account?
            <asp:LinkButton ID="btnRegister" runat="server" CssClass="footer-link" PostBackUrl="~/Register.aspx">Register</asp:LinkButton>
        </div>

    </div>
</asp:Content>
