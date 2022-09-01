using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public string PlaceOrder(PlaceOrderModel order, int userId)
        {
            try
            {
                return orderRL.PlaceOrder(order, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<OrderModel> GetAllOrders(int userId)
        {
            try
            {
                return orderRL.GetAllOrders(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveOrder(int orderId)
        {
            try
            {
                return orderRL.RemoveOrder(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
