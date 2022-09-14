using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApplication.Controllers
{
    [Route("")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("Adminlogin")]
        public IActionResult AdminLogin(AdminLoginModel admin)
        {
            try
            {
                var result = adminBL.AdminLogin(admin);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Login successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Login Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
