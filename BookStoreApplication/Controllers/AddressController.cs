using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Authorize]
    [Route("")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddressModel address)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = addressBL.AddAddress(address, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
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

        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel address)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = addressBL.UpdateAddress(address, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Updated", Data = result });
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
        
        [HttpGet("Getalladdress")]
        public IActionResult GetAllAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = addressBL.GetAllAddress(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Getting Address", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to get Address" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
