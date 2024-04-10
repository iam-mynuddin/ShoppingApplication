﻿using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository:IRepository<OrderDetail>
    {
        void Update(OrderDetail entity);
    }
}
