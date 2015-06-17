using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Handlers
{
    /// <summary>
    /// Summary description for getTypesNodeHandler
    /// </summary>
    public class getTypesNodeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int parentId = 0;
            int.TryParse(context.Request["id"], out parentId);
            List<Models.Category> types = null;
            try
            {
                //判断父节点的值
                if (parentId > 0)
                {
                    //加载子级菜单
                    types = CommonNews.Helper.OperateContext.Current.LoadSecondaryCategory(parentId);
                }
                else
                {
                    //加载顶级菜单
                    types = CommonNews.Helper.OperateContext.Current.LoadTopCategory();
                }
                //判断是否有值，有值的话先转换为tree模型再转换为json输出，没有值直接输出空字符串
                if (types != null)
                {
                    //转换为tree模型
                    List<Models.FormatModel.TreeModel> tree = types.Select(t => new Models.FormatModel.TreeModel() { id = t.CategoryId, text = t.CategoryName }).ToList();
                    //转换为json格式数据输出
                    context.Response.Write(Common.ConverterHelper.ObjectToJson(tree));
                }
                else
                {
                    context.Response.Write("");
                }
            }
            catch (Exception ex)
            {
                new Common.LogHelper(typeof(getTypesNodeHandler)).Error(ex);
                context.Response.Write("error");
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