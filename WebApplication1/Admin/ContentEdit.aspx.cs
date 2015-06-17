using System;
using System.Web.UI;

namespace WebApplication1.Admin
{
    public partial class ContentEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
            }
        }
        
        private void InitPage()
        {
            int id = -1;
            int.TryParse(Request.QueryString["id"], out id);
            if (id <= 0)
            {
                Response.Redirect("default.aspx");
            }
            NewsId.Value = Convert.ToString(id);
        }
    }
}