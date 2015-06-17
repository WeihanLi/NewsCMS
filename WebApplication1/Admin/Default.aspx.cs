using System;

namespace WebApplication1.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              ControlsDataBind();
        }

        private void ControlsDataBind()
        {
                repColumns.DataSource = CommonNews.Helper.OperateContext.Current.LoadNewsTypes();
                repColumns.DataBind();
        }

        protected void btnStatic_Click1(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CommonNews.Helper.OperateContext.StaticAll();
            Response.Write("<script>alert('静态化完成');window.location.href='/Admin/default.aspx'</script>");
        }
    }
}