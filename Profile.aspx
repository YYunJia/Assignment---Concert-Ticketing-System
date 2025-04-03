<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ConcertTicketing.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="profile-page">
        <div class="profile-container">
            <!-- Welcome Text -->
            <h2 class="welcome-text">Welcome, <asp:Label ID="lblWelcomeName" runat="server" Text="User"></asp:Label>!</h2>
            
            <!-- Navigation -->
            <div class="profile-layout">
                <div class="navigation">
                    <button class="category-button" id="btnPersonalInfo" runat="server" onserverclick="ShowPersonalInfo">Personal Info</button>
                    <button class="category-button" id="btnChangePwd" runat="server" onserverclick="ShowChangePassword">Change Password</button>                       
                    <button class="category-button" id="btnMembership" runat="server" onserverclick="ShowMembership">Membership</button>
                    <button class="category-button" id="btnPurchaseHistory" runat="server" onserverclick="ShowPurchaseHistory">Purchase History</button>
                </div>

                <!-- Content -->
                <div class="content">
                    <!-- Updated Personal Info Section -->
                    <div id="personalInfoSection" class="section active " runat="server">
                        <h3>Personal Information</h3>
                        <p>&nbsp;</p>
                        <p>
                            <strong>Name:</strong>
                            <asp:TextBox ID="txtName" runat="server" CssClass="input-field" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator 
                                ID="rfvName" 
                                runat="server" 
                                ControlToValidate="txtName" 
                                ForeColor="Red" 
                                ErrorMessage="Your Name is required." 
                                Display="Dynamic" />
                        </p>
                        <p>
                            <strong>Contact:</strong>
                            <asp:TextBox ID="txtContact" runat="server" CssClass="input-field" Enabled="false"></asp:TextBox>
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
                        </p>
                        <p>
                            <strong>Email:</strong>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="input-field" Enabled="false"></asp:TextBox>
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
                        </p>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" Visible="false" />
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>

                    <!-- Change Password Section -->
                    <div id="changePasswordSection" class="section" runat="server" style="display:none;">
                        <h3>Change Password</h3>
                        <p>&nbsp;</p>
                        <p>
                            <strong>Current Password:</strong>
                            <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="input-field" TextMode="Password"></asp:TextBox>
                        </p>
                        <p>
                            <strong>New Password:</strong>
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="input-field" TextMode="Password"></asp:TextBox>
                        </p>
                        <p>
                            <strong>Confirm New Password:</strong>
                            <asp:TextBox ID="txtConfirmNewPassword" runat="server" CssClass="input-field" TextMode="Password"></asp:TextBox>
                        </p>
                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn" OnClick="btnChangePassword_Click" />
                        <asp:Label ID="lblChangePasswordMsg" runat="server" CssClass="error-message" ForeColor="Red"></asp:Label>
                    </div>

                   <!-- Membership Section -->
                    <div id="membershipSection" class="section" runat="server" style="display:none;">
                        <h3>Membership Details</h3>
                        <p>&nbsp;</p>
                        <p><strong>Join Date:</strong> <asp:Label ID="lblJoinDate" runat="server"></asp:Label></p>
                        <p>&nbsp;</p>
                        <p><strong>Days as Member:</strong> <asp:Label ID="lblMembershipDays" runat="server"></asp:Label></p>
                        <p>&nbsp;</p>
                        <p><strong>Points Earned:</strong> <asp:Label ID="lblPointsEarned" runat="server"></asp:Label></p>
                        <p>&nbsp;</p>
                        <p><strong>Membership Tier:</strong> <asp:Label ID="lblMembershipTier" runat="server"></asp:Label></p>
                    </div>

                   <!-- Purchase History Section -->
                    <div id="purchaseHistorySection" class="section active" runat="server" style="display:none;">
                        <h3>Purchase History</h3>
                        <p>&nbsp;</p>
                        <asp:GridView ID="gvPurchaseHistory" runat="server" CssClass="purchase-history-table" AutoGenerateColumns="False" OnRowCommand="gvPurchaseHistory_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="BookingId" HeaderText="Booking ID" SortExpression="BookingId" />
                                <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" SortExpression="BookingDate" />
                                <asp:BoundField DataField="EventName" HeaderText="Event Name" SortExpression="EventName" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />

                                <asp:BoundField DataField="TicketId" HeaderText="Ticket ID" SortExpression="TicketId" />
                                <asp:BoundField DataField="SeatNumber" HeaderText="Seat Number" SortExpression="SeatNumber" />
                                <asp:BoundField DataField="TicketType" HeaderText="Ticket Type" SortExpression="TicketType" />

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewTicket" runat="server" Text="View Ticket" CommandName="ViewTicket" CommandArgument='<%# Eval("BookingId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:Label ID="lblNoPurchase" runat="server" Text="No purchase history available." Visible="false"></asp:Label>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

