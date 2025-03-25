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

            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    string newsId = hfNewsId.Value;
                    if (string.IsNullOrEmpty(newsId))
                    {
                        // Create Mode: Insert new news
                        InsertNews();
                        lblMessage.Text = "News has been added successfully!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        // Edit Mode: Update existing news
                        UpdateNews(newsId);
                        lblMessage.Text = "News has been updated successfully!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }


            private void InsertNews()
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\STARCONCERT.mdf;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Remove NewsId from the INSERT query since it's auto-generated
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO News (Title, BackgroundImage, preDesc, Content1, Content2, Content3, ContentImage1, ContentImage2, PublishBy, PublishedDate) VALUES (@Title, @BackgroundImage, @preDesc, @Content1, @Content2, @Content3, @ContentImage1, @ContentImage2, @PublishBy, @PublishedDate)", con))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@BackgroundImage", txtBackgroundImage.Text);
                        cmd.Parameters.AddWithValue("@preDesc", txtPreDescr.Text);
                        cmd.Parameters.AddWithValue("@Content1", txtContent1.Text);
                        cmd.Parameters.AddWithValue("@Content2", txtContent2.Text);
                        cmd.Parameters.AddWithValue("@Content3", txtContent3.Text);
                        cmd.Parameters.AddWithValue("@ContentImage1", txtContentImage1.Text);
                        cmd.Parameters.AddWithValue("@ContentImage2", txtContentImage2.Text);
                        cmd.Parameters.AddWithValue("@Content4", txtContent4.Text);
                        cmd.Parameters.AddWithValue("@Content5", txtContent5.Text);
                        cmd.Parameters.AddWithValue("@Content6", txtContent6.Text);
                        cmd.Parameters.AddWithValue("@Content7", txtContent7.Text);
                        cmd.Parameters.AddWithValue("@PublishBy", txtPublishBy.Text);
                        cmd.Parameters.AddWithValue("@PublishedDate", txtPublishedDate.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            private void UpdateNews(string newsId)
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\STARCONCERT.mdf;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE News SET Title = @Title, BackgroundImage = @BackgroundImage, preDesc = @preDesc, Content1 = @Content1, Content2 = @Content2, Content3 = @Content3, ContentImage1 = @ContentImage1, ContentImage2 = @ContentImage2, PublishBy = @PublishBy, PublishedDate = @PublishedDate WHERE NewsId = @NewsId", con))
                    {
                        cmd.Parameters.AddWithValue("@NewsId", newsId);
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@BackgroundImage", txtBackgroundImage.Text);
                        cmd.Parameters.AddWithValue("@preDesc", txtPreDescr.Text);
                        cmd.Parameters.AddWithValue("@Content1", txtContent1.Text);
                        cmd.Parameters.AddWithValue("@Content2", txtContent2.Text);
                        cmd.Parameters.AddWithValue("@Content3", txtContent3.Text);
                        cmd.Parameters.AddWithValue("@Content4", txtContent4.Text);
                        cmd.Parameters.AddWithValue("@Content5", txtContent5.Text);
                        cmd.Parameters.AddWithValue("@Content6", txtContent6.Text);
                        cmd.Parameters.AddWithValue("@Content7", txtContent7.Text);
                        cmd.Parameters.AddWithValue("@ContentImage1", txtContentImage1.Text);
                        cmd.Parameters.AddWithValue("@ContentImage2", txtContentImage2.Text);
                        cmd.Parameters.AddWithValue("@PublishBy", txtPublishBy.Text);
                        cmd.Parameters.AddWithValue("@PublishedDate", txtPublishedDate.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            protected void btnCancel_Click(object sender, EventArgs e)
            {
                Response.Redirect("staffDashboard.aspx");
            }
        }



    }
}
}