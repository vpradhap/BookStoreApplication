using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBL
    {
        public string PlaceOrder(PlaceOrderModel order, int userId);
        public List<OrderModel> GetAllOrders(int userId);
        public bool RemoveOrder(int orderId);
    }
}
