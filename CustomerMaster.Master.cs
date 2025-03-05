using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAssignment
{
    public partial class CustomerMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLoggedIn"] != null && (bool)Session["UserLoggedIn"])
            {
                // User is logged in, change the button to logout
                btnLoginRegister.Text = "Logout";
                btnLoginRegister.OnClientClick = "return confirm('Are you sure you want to log out?');"; // Optional logout confirmation
            }
            else
            {
                // User is not logged in, show the login/register button
                btnLoginRegister.Text = "Login/Register";
                btnLoginRegister.PostBackUrl = "~/LoginPage.aspx"; // Link to login/register page
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            // Search functionality can go here
        }

        // Logout functionality (Optional)
        protected void btnLoginRegister_Click(object sender, EventArgs e)
        {
            if (Session["UserLoggedIn"] != null && (bool)Session["UserLoggedIn"])
            {
                // Log the user out
                Session["UserLoggedIn"] = null;
                Response.Redirect("~/LoginPage.aspx"); // Redirect to login page after logout
            }
            else
            {
                // Redirect to login page if user is not logged in
                Response.Redirect("~/LoginPage.aspx");
            }
        }
    }
}