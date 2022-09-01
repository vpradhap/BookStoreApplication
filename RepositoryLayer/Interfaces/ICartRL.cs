using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        public AddToCartModel AddToCart(AddToCartModel cart, int userId);
        public string UpdateCart(int cartId, int bookQty);
        public bool RemoveFromCart(int cartId);
        public List<CartModel> GetCartItem(int userId);

    }
}
