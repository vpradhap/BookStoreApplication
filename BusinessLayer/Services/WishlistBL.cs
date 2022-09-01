using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }

        public string AddToWishlist(int bookId, int userId)
        {
            try
            {
                return wishlistRL.AddToWishlist(bookId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromWishlist(int wishlistId)
        {
            try
            {
                return wishlistRL.RemoveFromWishlist(wishlistId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<WishlistModel> GetWishlistItem(int userId)
        {
            try
            {
                return wishlistRL.GetWishlistItem(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
