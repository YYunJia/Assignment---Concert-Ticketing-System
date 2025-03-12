<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ConcertTicketing.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="register-container">
        <h2>Create Your Account</h2>

        <!-- Name (User Input) -->
        <div class="input-field">
            <label for="txtName">Full Name <span style="color: darkred;">*</span></label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Required="true" MaxLength="100" Placeholder="Enter your name"></asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvName" 
                runat="server" 
                ControlToValidate="txtName" 
                ForeColor="Red" 
                ErrorMessage="Your Name is required." 
                Display="Dynamic" />
        </div>

        <!-- Email (User Input) -->
        <div class="input-field">
            <label for="txtEmail">Email Address <span style="color: darkred;">*</span></label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Required="true" MaxLength="100" TextMode="Email" Placeholder="Enter your email (e.g., example@gmail.com)"></asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvEmail" 
                runat="server" 
                ControlToValidate="txtEmail" 
                ForeColor="Red" 
                ErrorMessage="Email Address is required." 
                Display="Dynamic" />
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
            <label for="txtContact">Contact Number <span style="color: darkred;">*</span></label>
            <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" MaxLength="50" Placeholder="Enter your contact number (e.g., 0101234567)"></asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvContact" 
                runat="server" 
                ControlToValidate="txtContact" 
                ForeColor="Red" 
                ErrorMessage="Contact Number is required." 
                Display="Dynamic" />
            <!-- RegularExpressionValidator for invalid characters (e.g., '-') -->
            <asp:RegularExpressionValidator 
                ID="regInvalidChars" 
                runat="server" 
                ControlToValidate="txtContact" 
                ValidationExpression="^[0-9]*$" 
                ForeColor="Red" 
                ErrorMessage="Please enter numbers only. Special characters like '-' are not allowed." 
                Display="Dynamic" />
        </div>

        <!-- Password (User Input) -->
        <div class="input-field">
            <label for="txtPassword">Password <span style="color: darkred;">*</span></label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" MaxLength="255" Placeholder="At least 8 chars, 1 uppercase, 1 number, 1 symbol"></asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="regPassword" 
                runat="server" 
                ControlToValidate="txtPassword" 
                ForeColor="Red" 
                ErrorMessage="Password is required." 
                Display="Dynamic" />
            <asp:RegularExpressionValidator
                ID="regPasswordStrength"
                runat="server"
                ControlToValidate="txtPassword"
                ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
                ForeColor="Red"
                ErrorMessage="Password must be at least 8 characters, include an uppercase letter, a number, and a special character."
                Display="Dynamic" />
        </div>

        <!-- Confirm Password (User Input for Validation) -->
        <div class="input-field">
            <label for="txtConfirmPassword">Confirm Password <span style="color: darkred;">*</span></label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" MaxLength="255"  Placeholder="Re-enter your password"></asp:TextBox>
            <asp:CompareValidator 
                ID="cvConfirmPassword"
                runat="server"
                ControlToValidate="txtConfirmPassword"
                ControlToCompare="txtPassword"
                ForeColor="Red"
                ErrorMessage="Passwords do not match."
                Display="Dynamic" />
            <asp:RequiredFieldValidator 
                ID="rfvConfirmPwd" 
                runat="server" 
                ControlToValidate="txtConfirmPassword" 
                ForeColor="Red" 
                ErrorMessage="Please enter the confirm password." 
                Display="Dynamic" />
        </div>

        <!-- Role (Hidden or predefined as 'Customer') -->
        <div class="input-field" style="display:none;">
            <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" Text="Customer" ReadOnly="true"></asp:TextBox>
        </div>

        <!-- Register Button -->
        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn" OnClick="btnRegister_Click" />

        <!-- Already Have an Account -->
        <div class="footer-text">
            Already have an account? <a href="LoginPage.aspx">Login</a>
        </div>
    </div>
</asp:Content>
