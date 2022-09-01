using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishlistBL
    {
        public string AddToWishlist(int bookId, int userId);
        public bool RemoveFromWishlist(int wishlistId);
        public List<WishlistModel> GetWishlistItem(int userId);
    }
}
