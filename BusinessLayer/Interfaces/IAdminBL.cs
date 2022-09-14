using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAdminBL
    {
        public AdminModel AdminLogin(AdminLoginModel admin);
    }
}
