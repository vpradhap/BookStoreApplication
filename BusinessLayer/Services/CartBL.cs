using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public AddToCartModel AddToCart(AddToCartModel cart, int userId)
        {
            try
            {
                return cartRL.AddToCart(cart, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UpdateCart(int cartId, int bookQty)   
        {
            try
            {
                return cartRL.UpdateCart(cartId, bookQty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromCart(int cartId)
        {
            try
            {
                return cartRL.RemoveFromCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CartModel> GetCartItem(int userId)
        {
            try
            {
                return cartRL.GetCartItem(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
