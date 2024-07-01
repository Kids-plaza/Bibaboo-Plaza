﻿using BPA.BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA.DAO
{
    public class ProductDAO:BaseDAO<Product>
    {
        private static ProductDAO instance = null;
        private static readonly object lockObject = new();

        public ProductDAO()
        {
        }
        public static ProductDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance != null)
                    {
                        return instance;
                    }
                    instance = new ProductDAO();
                    return instance;
                }
            }
        }
    }
}
