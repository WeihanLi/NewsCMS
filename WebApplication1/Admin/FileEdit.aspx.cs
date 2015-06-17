using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Admin
{
    public partial class FileEdit : System.Web.UI.Page
    {
        protected int type = -1;
        protected string fName = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ControlsDataBind();
            }
        }

        private void ControlsDataBind()
        {
            int.TryParse(Request.QueryString["t"], out type);
            fName = Request.QueryString["fName"];
            string text = CommonNews.Helper.OperateContext.Current.GetFileContent((Common.FileType)type, fName);
            code.Value = text;
        }
    }
}