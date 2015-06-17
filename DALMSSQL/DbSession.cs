using IDAL;
using System;

namespace DALMSSQL
{
    public class DbSession : IDbSession
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

        public IUser IUser
        {
            get
            {
                if (iUser == null)
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

        public int SaveChanges()
        {
            //获取上下文对象，保存数据，更新数据库
            return new DbContextFactory().GetDbContext().SaveChanges();
        }
    }
}
