using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL : ICartRL
    {
        public IConfiguration Configuration { get; }
        public CartRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AddToCartModel AddToCart(AddToCartModel cart, int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddToCart", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookInCart", cart.BookInCart);
                command.Parameters.AddWithValue("@BookId", cart.BookId);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result == 1)
                {
                    return cart;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UpdateCart(int cartId, int bookQty)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spUpdateCart", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookInCart", bookQty);
                command.Parameters.AddWithValue("@CartId", cartId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return "Quantity updated";
                }
                else
                {
                    return "Failed to update";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromCart(int cartId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spRemoveFromCart", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CartId", cartId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CartModel> GetCartItem(int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                List<CartModel> cartList = new List<CartModel>();
                SqlCommand command = new SqlCommand("spGetAllCartItem", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CartModel cart = new CartModel();
                        CartModel temp = GetCartDetails(cart, reader);
                        cartList.Add(temp);
                    }
                    return cartList;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CartModel GetCartDetails(CartModel cart, SqlDataReader rdr)
        {
            cart.BookId = Convert.ToInt32(rdr["BookId"] == DBNull.Value ? default : rdr["BookId"]);
            cart.UserId = Convert.ToInt32(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
            cart.CartId = Convert.ToInt32(rdr["CartId"] == DBNull.Value ? default : rdr["CartId"]);
            cart.BookName = Convert.ToString(rdr["BookName"] == DBNull.Value ? default : rdr["BookName"]);
            cart.AuthorName = Convert.ToString(rdr["AuthorName"] == DBNull.Value ? default : rdr["AuthorName"]);
            cart.BookImage = Convert.ToString(rdr["BookImage"] == DBNull.Value ? default : rdr["BookImage"]);
            cart.DiscountPrice = Convert.ToInt32(rdr["DiscountPrice"] == DBNull.Value ? default : rdr["DiscountPrice"]);
            cart.OriginalPrice = Convert.ToInt32(rdr["OriginalPrice"] == DBNull.Value ? default : rdr["OriginalPrice"]);
            cart.BookInCart = Convert.ToInt32(rdr["BookInCart"] == DBNull.Value ? default : rdr["BookInCart"]);
            return cart;
        }
    }
}
