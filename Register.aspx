<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebAssignment.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="register-container">
        <h2>Create Your Account</h2>

        <!-- Name (User Input) -->
        <div class="input-field">
            <label for="txtName">Full Name</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Required="true" MaxLength="100"></asp:TextBox>
        </div>

        <!-- Email (User Input) -->
        <div class="input-field">
            <label for="txtEmail">Email Address</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Required="true" MaxLength="100" TextMode="Email"></asp:TextBox>
        </div>

        <!-- Contact (User Input) -->
        <div class="input-field">
            <label for="txtContact">Contact Number</label>
                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" MaxLength="50" Placeholder=" (e.g., 0101234567)"></asp:TextBox>
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
        </div>

        <!-- Register Button -->
        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn" OnClick="btnRegister_Click" />

        <!-- Already Have an Account -->
        <div class="footer-text">
            Already have an account? <a href="LoginPage.aspx">Login</a>
        </div>
    </div>
</asp:Content>
