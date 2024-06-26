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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Add(Order order)
        {
            _orderRepository.Add(order);
        }

        public void Delete(Order order)
        {
            _orderRepository.Delete(order);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order? GetById(Guid id)
        {
            return _orderRepository.GetById(id);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public void Update2(Order order)
        {
            _orderRepository.Update(order);
        }

    }
}
