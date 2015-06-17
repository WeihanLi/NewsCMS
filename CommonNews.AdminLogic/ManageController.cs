using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CommonNews.AdminLogic
{
    [Common.Filters.PermissionRequired]
    [ValidateInput(false)]
    public class ManageController : Controller
    {
        /// <summary>
        /// 管理首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ColumnManage()
        {
            return View();
        }

        /// <summary>
        /// 内容管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContentManage()
        {
            return View();
        }

        /// <summary>
        /// 加载新闻类别
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadNewsTypes()
        {
            List<Models.Category> types = Helper.OperateContext.Current.LoadNewsTypes();
            //Todo:直接转换为json可能存在问题
            return Json(types);
        }

        /// <summary>
        /// 添加新闻类别
        /// </summary>
        /// <param name="typeName">新闻类别名称</param>
        /// <param name="indexName">类别索引名称</param>
        /// <param name="parentId">父节点id</param>
        /// <returns></returns>
        public ActionResult AddNewsType(string typeName, string indexName, int parentId)
        {
            Models.Category type = new Models.Category() { CategoryName = typeName, ParentId = parentId, IndexName = indexName };
            if (Helper.OperateContext.Current.AddNewsType(type))
            {
                return RedirectToAction("ColumnManage");
            }
            else
            {
                return Content("<script>alert('添加失败，请稍后重试')</script>");
            }
        }

        /// <summary>
        /// 更新新闻类别
        /// </summary>
        /// <param name="tId">类别id</param>
        /// <param name="typeName">类型名称</param>
        /// <param name="indexName">类别索引名称</param>
        /// <param name="parentId">父节点id</param>
        /// <returns></returns>
        public ActionResult UpdateNewsType(int tId, string typeName, string indexName, int parentId)
        {
            Models.Category t = new Models.Category() { CategoryId = tId, CategoryName = typeName, IndexName = indexName, ParentId = parentId };
            if (Helper.OperateContext.Current.UpdateNewsType(t))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        /// <summary>
        /// 删除新闻类别
        /// </summary>
        /// <param name="tId">新闻类别id</param>
        /// <returns></returns>
        public ActionResult DeleteNewsType(int tId)
        {
            if (Helper.OperateContext.Current.DelNewsType(tId))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        /// <summary>
        /// 分页加载新闻列表
        /// </summary>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="typeId">分类id</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        public ActionResult LoadNews(int pageIndex, int pageSize, int typeId, int order)
        {
            List<Models.News> news = Helper.OperateContext.Current.LoadNews(pageIndex, pageSize, typeId, (order > 0) ? true : false);
            return Json(news);
        }

        /// <summary>
        /// 分页加载回收站中的新闻数据
        /// </summary>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="typeId">新闻类别id</param>
        /// <param name="order">排列顺序</param>
        /// <returns></returns>
        public ActionResult LoadRecycleBinNews(int pageIndex, int pageSize, int typeId, int order)
        {
            List<Models.News> news = Helper.OperateContext.Current.LoadNews(pageIndex, pageSize, typeId, (order > 0) ? true : false);
            return Json(news);
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AddNews(Models.ViewModel.AddNewsViewModel model)
        {
            Models.News news = new Models.News() { NewsTitle = model.NewsTitle, NewsContent = model.NewsContent, NewsTypeId = model.NewsTypeId, IsTop = (model.IsTop > 0) ? true : false, NewsAttachPath = model.NewsAttachPath, NewsImagePath = model.NewsImagePath, NewsExternalLink = model.NewsExternalPath };
            if (Helper.OperateContext.Current.AddNews(news))
            {
                return RedirectToAction("ContentManage");
            }
            else
            {
                return Content("<script>alert('操作失败，请稍后重试')</script>");
            }
        }

        /// <summary>
        /// 将新闻移动到回收站
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public ActionResult RemoveToRecycleBin(int id)
        {
            if (Helper.OperateContext.Current.MoveToRecycleBin(id))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        /// <summary>
        /// 彻底删除新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public ActionResult DeleteNewsCompletely(int id)
        {
            if (Helper.OperateContext.Current.DelNewsCompletely(id))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        /// <summary>
        /// 清空回收站中的新闻
        /// </summary>
        /// <returns></returns>
        public ActionResult EmptyRecycleBin()
        {
            if (Helper.OperateContext.Current.EmptyRecycleBin())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("<script>alert('操作失败，请稍后重试')</script>");
            }
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public ActionResult UpdateNews(Models.ViewModel.NewsViewModel model)
        {
            if (Helper.OperateContext.Current.UpdateNews(model.ToNewsModel()))
            {
                return RedirectToAction("ContentManage");
            }
            else
            {
                return Content("<script>alert('操作失败，请稍后重试')</script>");
            }
        }
    }
}
