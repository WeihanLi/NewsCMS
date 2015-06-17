using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonNews.Logic
{
    public class HomeController: Controller
    {
        public ActionResult Index()
        {
            return Content("Hello MVC") ;
        }

        public JsonResult GetVisitCount(int newsId)
        {
            return Json(Helper.OperateContext.Current.GetNewsVisitCount(newsId));
        }
    }
}
