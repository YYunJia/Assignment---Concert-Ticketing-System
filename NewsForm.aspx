<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NewsForm.aspx.cs" Inherits="ConcertTicketing.NewsForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffMainContent" runat="server">
    <div class="newsForm">
    <h1><asp:Literal ID="litFormTitle" runat="server"></asp:Literal></h1>
    <asp:HiddenField ID="hfNewsId" runat="server" />
    <div>
        <label>Title:</label><br />
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Background Image:</label>
        <asp:TextBox ID="txtBackgroundImage" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Pre Description:</label><br />
        <asp:TextBox ID="txtPreDescr" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
        <label>Content 1:</label><br />
        <asp:TextBox ID="txtContent1" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
        <label>Content 2:</label><br />
        <asp:TextBox ID="txtContent2" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
        <label>Content 3:</label><br />
        <asp:TextBox ID="txtContent3" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
    <label>Content 4:</label><br />
        <asp:TextBox ID="txtContent4" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
    <label>Content 5:</label><br />
        <asp:TextBox ID="txtContent5" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
    <label>Content 6:</label><br />
        <asp:TextBox ID="txtContent6" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>

     <div>
    <label>Content 7:</label><br />
        <asp:TextBox ID="txtContent7" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
        <label>Content Image 1:</label><br />
        <asp:TextBox ID="txtContentImage1" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Content Image 2:</label><br />
        <asp:TextBox ID="txtContentImage2" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Published By:</label><br />
        <asp:TextBox ID="txtPublishBy" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Published Date:</label><br />
        <asp:TextBox ID="txtPublishedDate" runat="server" TextMode="Date"></asp:TextBox>
    </div>
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-cancel" />
</div>
    <br />
    <!-- Label for displaying success or error messages -->
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
    <br />
    <br />

</asp:Content>
