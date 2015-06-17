using System.Web.Mvc;

namespace CommonNews.AdminLogic
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.ViewModel.LoginViewModel loginModel)
        {
            if (Helper.OperateContext.Current.Login(loginModel))
            {
                return RedirectToAction("Index","Manage");
            }
            else
            {
                return Content("<script>alert('login fail!');location.href='/Admin/Account/Login'</script>");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return View("Login");
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCaptcha()
        {
            //Todo:验证码
            return null;
        }

        public bool ValidCaptcha(string code)
        {
            //Todo:验证验证码

            return false;
        }
    }
}
