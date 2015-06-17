using System;
using System.Web.UI;

namespace WebApplication1.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("login.html");
            }
            else if (Session["Admin"]==null)
            {
                string script = "<script>var item = document.getElementById('user_manage');item.style.display='none'</script>";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "load", script);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "init", script);
            }
        }
    }
}