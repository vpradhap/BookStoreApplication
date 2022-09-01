using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserRegisterModel Register(UserRegisterModel user)
        {
            try
            {
                return userRL.Register(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LoginResponseModel Login(LoginModel user)
        {
            try
            {
                return userRL.Login(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ForgotPassword(string emailId)
        {
            try
            {
                return userRL.ForgotPassword(emailId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ResetPassword(ResetPasswordModel user)
        {
            try
            {
                return userRL.ResetPassword(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

    }
}
