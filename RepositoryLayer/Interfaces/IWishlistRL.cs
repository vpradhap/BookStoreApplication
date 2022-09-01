using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishlistRL
    {
        public string AddToWishlist(int bookId, int userId);
        public bool RemoveFromWishlist(int wishlistId);
        public List<WishlistModel> GetWishlistItem(int userId);
    }
}
