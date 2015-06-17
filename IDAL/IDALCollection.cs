namespace IDAL
{
    public interface IUser : IBaseDAL<Models.User>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="u">用户登录实体</param>
        /// <returns></returns>
        bool Login(Models.User u);
    }
    
    public interface INews : IBaseDAL<Models.News>
    {
        /// <summary>
        /// 新增数据、返回主键值
        /// </summary>
        /// <param name="news">新闻实体</param>
        /// <returns></returns>
        int AddDataWithIdReturn(Models.News news);
    }

    public interface ICategory : IBaseDAL<Models.Category> { }
}
