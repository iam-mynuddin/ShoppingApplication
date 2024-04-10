using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.DataAccess.Data;
using ShoppingApp.DataAccess.Repository.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShoppingApp.DataAccess.Repository
{
    public class Repository <T>: IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            this._db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? strIncludeProp = null, bool track = false)
        {
            IQueryable<T> query;
            if (track)
            {
                query = dbSet;                
            }
            else
            {
                query = dbSet.AsNoTracking();                
            }
            if (!String.IsNullOrEmpty(strIncludeProp))
            {
                foreach (var prop in strIncludeProp.Split(','))
                {
                    if (string.IsNullOrEmpty(prop)) continue;//do not iterate for null or empty values
                    query = query.Include(prop);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
        public IEnumerable<T> GetMultiple(Expression<Func<T, bool>> filter, string? strIncludeProp = null, bool track = false)
        {
            IQueryable<T> query;
            if (track)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            if (!String.IsNullOrEmpty(strIncludeProp))
            {
                foreach (var prop in strIncludeProp.Split(','))
                {
                    if (string.IsNullOrEmpty(prop)) continue;//do not iterate for null or empty values
                    query = query.Include(prop);
                }
            }
            query = query.Where(filter);
            return query;
        }
        public IEnumerable<T> GetAll(string? strIncludeProp=null)
        {
            IQueryable<T> query = dbSet;
            if (!String.IsNullOrEmpty(strIncludeProp))
            {
                foreach (var prop in strIncludeProp.Split(','))
                {
                    if (string.IsNullOrEmpty(prop)) continue;//do not iterate for null or empty values
                    query=query.Include(prop);
                }
            }
            return query;
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
