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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public void Add(OrderDetail orderDetail)
        {
            _orderDetailRepository.Add(orderDetail);
        }

        public void Update(OrderDetail orderDetail) 
        {
            _orderDetailRepository.Update(orderDetail);
        }

        public void Delete(OrderDetail orderDetail)
        {
            _orderDetailRepository.Delete(orderDetail);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public OrderDetail? GetById(Guid id)
        {
            return _orderDetailRepository.GetById(id);
        }

    }
}
