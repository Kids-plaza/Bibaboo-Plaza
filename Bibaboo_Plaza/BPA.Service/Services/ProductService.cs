﻿using BPA.BusinessObject.Entities;
using BPA.Repository.IRepositories;
using BPA.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Add(Product product)
        {
            _productRepository.Add(product);
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product? GetById(Guid id)
        {
            return _productRepository.GetById(id);
        }

        public IEnumerable<Product> SearchByName(string name)
        {
           return _productRepository.GetAll().Where(x => x.ProductName!.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
        }

        public void Update2(Product product)
        {
            _productRepository.Update2(product);
        }
    }
}
