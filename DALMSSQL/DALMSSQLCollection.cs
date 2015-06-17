namespace DALMSSQL
{
    public class User : BaseDAL<Models.User>, IDAL.IUser
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="u">登录视图模型实体</param>
        /// <returns>登录状态</returns>
        public bool Login(Models.User u)
        {
            //判断用户是否经过验证，如果没有，返回 需要验证 Model.LoginState.RequireVerification
            // return Model.LoginState.RequireVerification
            if (Exist(user => user.UserName == u.UserName && user.UserPassword == u.UserPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class News : BaseDAL<Models.News>, IDAL.INews
    {
        /// <summary>
        /// 新增数据、返回主键值
        /// </summary>
        /// <param name="news">新闻实体</param>
        /// <returns></returns>
        public int AddDataWithIdReturn(Models.News news)
        {
            db.Set<Models.News>().Add(news);
            db.SaveChanges();
            return news.NewsId;
        }
    }

    public class Category : BaseDAL<Models.Category>, IDAL.ICategory
    {
    }
}