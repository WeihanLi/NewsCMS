using System;
using System.Web;
using System.Web.SessionState;

namespace WebApplication1.Admin
{
    /// <summary>
    /// LoginHandler
    /// </summary>
    public class LoginHandler : IHttpHandler,IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string uid = context.Request["username"], pwd = context.Request["password"],action=context.Request["action"];
            if (String.IsNullOrEmpty(uid)||String.IsNullOrEmpty(pwd))
            {
                context.Response.Write("<html><body><script>alert('用户名或密码不完整，请重试');location.href='/Admin/login.html'</script></body></html>");
                context.Response.End();
                return;
            }
            Models.ViewModel.LoginViewModel model = new Models.ViewModel.LoginViewModel() { UserName = uid, Password = pwd };
            if (action.Equals("login"))
            {
                if (CommonNews.Helper.OperateContext.Current.Login(model))
                {

                    context.Response.Redirect("~/Admin/Default.aspx");
                }
                else
                {
                    context.Response.Write("<html><body><script>alert('用户名或密码错误');location.href='/Admin/login.html'</script></body></html>");
                }
            }
            else
            {
                if (CommonNews.Helper.OperateContext.Current.Login(model))
                {
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }
          
            
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}