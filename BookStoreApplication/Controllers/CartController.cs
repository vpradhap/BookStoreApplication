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
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost("Addtocart")]
        public IActionResult AddToCart(AddToCartModel cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.AddToCart(cart, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Added to Cart", Data = result });
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

        [HttpPut("Updatecart")]
        public IActionResult UpdateCart(int cartId, int bookQty)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.UpdateCart(cartId, bookQty);
                if (result != null)
                {
                    return this.Ok(new { Status = true,Message = "Quantity updated" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to update" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("Removefromcart")]
        public IActionResult RemoveFromCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.RemoveFromCart(cartId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Removed from Cart" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to remove" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("Getcartitem")]
        public IActionResult GetCartItem()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = cartBL.GetCartItem(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart data", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to fetch" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
