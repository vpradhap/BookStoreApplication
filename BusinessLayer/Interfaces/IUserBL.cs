using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        UserRegisterModel Register(UserRegisterModel user);
        public LoginResponseModel Login(LoginModel user);
        public string ForgotPassword(string emailId);
        public string ResetPassword(ResetPasswordModel user);

    }
}
