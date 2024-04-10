using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingApp.DataAccess.Data;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            this._db = db;            
        }   
        public void Update(Product entity)
        {
            var objFromDb = _db.Products.FirstOrDefault(x => x.ProductId == entity.ProductId);
            if (objFromDb != null)
            {
                if(entity.ImgUrl==null)
                {
                    entity.ImgUrl = objFromDb.ImgUrl;
                }
            }
            _db.Update(entity);
        }
        public Product GetProductWithCategory(Expression<Func<Product, bool>> filter) 
        {
            Product obj;
            obj= (Product)_db.Products.Include(x=>x.Category).Where(filter);
            return obj;
        }
    }
}
