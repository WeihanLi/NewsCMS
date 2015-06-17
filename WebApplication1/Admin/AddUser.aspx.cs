using System;
using System.Web.UI;

namespace WebApplication1.Admin
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Models.User u = new Models.User() { UserName= txtUid.Text,UserPassword=txtPwd.Text,AddTime=DateTime.Now};
            if (String.IsNullOrEmpty(u.UserName)||String.IsNullOrEmpty(u.UserPassword)||!cvPwd.IsValid)
            {
                return;
            }
            u.UserPassword = Common.SecurityHelper.SHA1_Encrypt(u.UserPassword);
            
            if (CommonNews.Helper.OperateContext.Current.ExistUserName(u.UserName))
            {
                Response.Write("<script>alert('添加失败！该用户名已经存在')</script>");
                return;
            }
            if (CommonNews.Helper.OperateContext.Current.AddUser(u))
            {
                Response.Redirect("UserManage.aspx");
            }
            else
            {
                Response.Write("<script>alert('添加失败！请稍后重试')</script>");
            }
        }
    }
}