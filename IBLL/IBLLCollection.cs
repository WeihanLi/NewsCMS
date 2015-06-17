namespace IBLL
{
    public interface IUser :IBaseBLL<Models.User>
    {
        bool Login(Models.ViewModel.LoginViewModel model);

        //bool Regist(Models.ViewModel.RegisterViewModel model);

        //bool ActivateAccount(Models.User u);
    }

    public interface INews : IBaseBLL<Models.News>
    {
        /// <summary>
        /// 新增数据、返回主键值
        /// </summary>
        /// <param name="news">新闻实体</param>
        /// <returns>新增数据主键</returns>
        int AddDataWithIdReturn(Models.News news);
    }

    public interface ICategory : IBaseBLL<Models.Category> { }
}
