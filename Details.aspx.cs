using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConcertTicketing
{
	public partial class Details : System.Web.UI.Page
	{

        private string connectionString = ConfigurationManager.ConnectionStrings["AConnectionString"].ConnectionString;
        private string eventId;
        protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}