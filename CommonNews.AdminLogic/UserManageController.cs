using System.Collections.Generic;
using System.Web.Mvc;

namespace CommonNews.AdminLogic
{
    [Common.Filters.AdminPermissionRequired]
    public class UserManageController:Controller
    {
        #region UserOperation

        public ActionResult Index()
        {
            //加载用户列表
            List<Models.User> users = Helper.OperateContext.Current.SelectUser();
            return View();
        }

        /// <summary>
        /// 添加用户    Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        /// <summary>
        /// 添加用户    POST
        /// </summary>
        /// <param name="model">添加用户实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUser(Models.ViewModel.RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content("<script>alert('输入数据格式有误')</script>");
            }
            //
            Models.User u = new Models.User() { UserName = model.Name, UserPassword = Common.SecurityHelper.SHA1_Encrypt(model.Password) };
            if (Helper.OperateContext.Current.AddUser(u))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("<script>alert('添加失败，请稍后重试')</script>");
            }
        }

        /// <summary>
        /// 修改密码    Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdatePwd()
        {
            return View();
        }

        /// <summary>
        /// 修改密码    POST
        /// </summary>
        /// <param name="model">重置密码模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePwd(Models.ViewModel.ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }
            //Todo:补充
            Models.User u = new Models.User() { UserName=model.Username,UserPassword=model.Password};
            if (Helper.OperateContext.Current.UpdateUserPwd(u))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public ActionResult DisableUser(int userId)
        {
            if (userId<=0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if (Helper.OperateContext.Current.DisableUser(userId))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public ActionResult EnableUser(int userId)
        {
            if (userId <= 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if (Helper.OperateContext.Current.EnableUser(userId))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 永久移除用户、删除用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public ActionResult RemoveUserCompletely(int userId)
        {
            //
            Models.User u = new Models.User() { UserId = userId };
            if (Helper.OperateContext.Current.DeleteUser(u))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
