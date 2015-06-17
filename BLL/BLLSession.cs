using System;
using IBLL;

namespace BLL
{
    public class BLLSession : IBLL.IBLLSession
    {
        #region Fields

        private INews iNews = null;
        private ICategory iCategory = null;
        private IUser iUser = null;

        #endregion

        public INews INews
        {
            get
            {
                if (iNews == null)
                {
                    iNews = new News();
                }
                return iNews;
            }

            set
            {
                iNews = value;
            }
        }


        public IUser IUser
        {
            get
            {
                if (iUser==null)
                {
                    iUser = new User();
                }
                return iUser;
            }

            set
            {
                iUser = value;
            }
        }

        public ICategory ICategory
        {
            get
            {
                if (iCategory == null)
                {
                    iCategory = new Category();
                }
                return iCategory;
            }

            set
            {
                iCategory = value;
            }
        }

        public int SaveChanges()
        {
            //1.根据配置文件内容 创建 DBSessionFactory 工厂对象
            IDAL.IDbSessionFactory sessionFactory = null;

            //2.通过 工厂 创建 DBSession对象
            IDAL.IDbSession iDbSession = sessionFactory.GetDbSession();

            return iDbSession.SaveChanges();
        }
    }
}
