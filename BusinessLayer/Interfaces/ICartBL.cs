using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        public AddToCartModel AddToCart(AddToCartModel cart, int userId);
        public string UpdateCart(int cartId, int bookQty);
        public bool RemoveFromCart(int cartId);
        public List<CartModel> GetCartItem(int userId);

    }
}
