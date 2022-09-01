using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public UserRegisterModel Register(UserRegisterModel user);
        public LoginResponseModel Login(LoginModel user);
        public string ForgotPassword(string emailId);
        public string ResetPassword(ResetPasswordModel user);

    }
}
