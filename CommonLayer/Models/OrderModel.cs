﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int OrderQty { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public string AuthorName { get; set; }
    }
}
