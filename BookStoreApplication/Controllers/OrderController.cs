using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Authorize(Roles = Role.User)]
    [Route("")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost("Placeorder")]
        public IActionResult PlaceOrder(PlaceOrderModel order)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = orderBL.PlaceOrder(order, userId);
                if (result != null )
                {
                    return this.Ok(new { Status = true, Message = "Order Placed"});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to add" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("Getorders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = orderBL.GetAllOrders(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Details", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to get" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteOrder")]
        public IActionResult RemoveOrder(int orderId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = orderBL.RemoveOrder(orderId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Deleted" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to delete" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
