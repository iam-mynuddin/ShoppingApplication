using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T can be any class. We can perform operations on any table because here we are using generic class.
        IEnumerable<T> GetAll(string? strIncludeProp = null);
        public IEnumerable<T> GetMultiple(Expression<Func<T, bool>> filter, string? strIncludeProp = null, bool track = false);
        T Get(Expression<Func<T, bool>> filter, string? strIncludeProp = null,bool track=false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
