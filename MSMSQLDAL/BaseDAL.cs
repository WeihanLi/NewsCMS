using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSMSQLDAL
{
    public class BaseDAL<T> : IDAL.IBaseDAL<T> where T : class, new()
    {
        public int Add(T model)
        {
            throw new NotImplementedException();
        }

        public int Del(T model)
        {
            throw new NotImplementedException();
        }

        public int DelBy(Expression<Func<T, bool>> delWhere)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public List<T> GetList()
        {
            throw new NotImplementedException();
        }

        public List<T> GetListBy(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda)
        {
            throw new NotImplementedException();
        }

        public List<T> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isAsc = true)
        {
            throw new NotImplementedException();
        }

        public List<T> GetPagedList<TKey>(int pageIndex, int pageSize, ref int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isAsc = true)
        {
            throw new NotImplementedException();
        }

        public int Modify(T model, params string[] proNames)
        {
            throw new NotImplementedException();
        }

        public int ModifyBy(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        {
            throw new NotImplementedException();
        }
    }
}
