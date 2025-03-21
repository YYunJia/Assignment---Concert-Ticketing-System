using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConcertTicketing
{
	public partial class NewsForm : System.Web.UI.Page
	{
        public partial class NewsForm : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    string newsId = Request.QueryString["NewsId"];
                    if (!string.IsNullOrEmpty(newsId))
                    {
                        // Edit Mode: Load existing news details
                        litFormTitle.Text = "Edit News";
                        LoadNews(newsId);
                    }
                    else
                    {
                        // Create Mode: Set form title
                        litFormTitle.Text = "Create News";
                    }
                }
            }

            private void LoadNews(string newsId)
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\STARCONCERT.mdf;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM News WHERE NewsId = @NewsId", con))
                    {
                        cmd.Parameters.AddWithValue("@NewsId", newsId);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hfNewsId.Value = reader["NewsId"].ToString();
                                txtTitle.Text = reader["Title"].ToString();
                                txtBackgroundImage.Text = reader["BackgroundImage"].ToString();
                                txtPreDescr.Text = reader["preDesc"].ToString();
                                txtContent1.Text = reader["Content1"].ToString();
                                txtContent2.Text = reader["Content2"].ToString();
                                txtContent3.Text = reader["Content3"].ToString();
                                txtContentImage1.Text = reader["ContentImage1"].ToString();
                                txtContentImage2.Text = reader["ContentImage2"].ToString();
                                txtContent4.Text = reader["Content4"].ToString();
                                txtContent5.Text = reader["Content5"].ToString();
                                txtContent6.Text = reader["Content6"].ToString();
                                txtContent7.Text = reader["Content7"].ToString();
                                txtPublishBy.Text = reader["PublishBy"].ToString();
                                txtPublishedDate.Text = Convert.ToDateTime(reader["PublishedDate"]).ToString("yyyy-MM-dd");
                            }
                        }
                    }
                }
            }

        }
    }