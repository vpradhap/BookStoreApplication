using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Authorize(Roles =Role.User)]
    [Route("")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;
        public WishlistController(IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        [HttpPost("Addtowishlist")]
        public IActionResult AddToWishlist(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = wishlistBL.AddToWishlist(bookId, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Data = result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("Removefromwishlist")]
        public IActionResult RemoveFromWishlist(int wishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = wishlistBL.RemoveFromWishlist(wishlistId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Removed from wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("Getwishlistitem")]
        public IActionResult GetWishlistItem()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = wishlistBL.GetWishlistItem(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
