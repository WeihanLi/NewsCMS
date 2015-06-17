using Models.ViewModel;

namespace BLL
{
    public class User : BaseBLL<Models.User>,IBLL.IUser
    {
        public bool Login(LoginViewModel model)
        {
            //用户密码 加密 SHA1_Encrypt
            //获取加密后密码,将密码设置为加密后密码
            Models.User u = new Models.User() { UserName = model.UserName, UserPassword =Common.SecurityHelper.SHA1_Encrypt(model.Password) };
            return (IDal as IDAL.IUser).Login(u);
        }

        public override void SetDal()
        {
            IDal = DbSession.IUser;
        }
        
    }

    public class News : BaseBLL<Models.News>, IBLL.INews
    {
        public override void SetDal()
        {
            IDal = DbSession.INews;
        }

        /// <summary>
        /// 新增数据、返回主键值
        /// </summary>
        /// <param name="news">新闻实体</param>
        /// <returns></returns>
        public int AddDataWithIdReturn(Models.News news)
        {
            return (IDal as IDAL.INews).AddDataWithIdReturn(news);
        }
    }

    public class Category : BaseBLL<Models.Category>, IBLL.ICategory
    {
        public override void SetDal()
        {
            IDal = DbSession.ICategory;
        }
    }
}
