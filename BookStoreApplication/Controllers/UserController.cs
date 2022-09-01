using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace BookStoreApplication.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserRegisterModel user)
        {
            try
            {
                var result = userBL.Register(user);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "User Registration successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to Register" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel user)
        {
            try
            {
                var result = userBL.Login(user);
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

        [HttpPost]
        [Route("Forgotpassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = userBL.ForgotPassword(email);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Token sent to your mail successfully" });
                }
                else
                {
                    return this.NotFound(new { Status = false, Message = "Incorrect email or password" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Resetpassword")]
        public IActionResult ResetPassword(ResetPasswordModel user)
        {
            try
            {
                user.EmailId = User.FindFirst(ClaimTypes.Email).Value;
                var result = userBL.ResetPassword(user);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Reset success" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Reset failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
