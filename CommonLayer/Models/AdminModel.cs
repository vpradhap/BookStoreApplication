using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AdminModel
    {
        public int AdminId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }
        public string token { get; set; }
    }
}
