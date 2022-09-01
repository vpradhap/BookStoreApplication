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
    public class OrderRL : IOrderRL
    {
        public IConfiguration Configuration { get; }

        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string PlaceOrder(PlaceOrderModel order, int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddOrder", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AddressId", order.AddressId);
                command.Parameters.AddWithValue("@BookId", order.BookId);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result == 3)
                {
                    return "Order Placed";
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
        public List<OrderModel> GetAllOrders(int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                List<OrderModel> orderList = new List<OrderModel>();
                SqlCommand command = new SqlCommand("spGetAllOrders", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModel order = new OrderModel();
                        order.OrderId = Convert.ToInt32(reader["OrderId"] == DBNull.Value ? default : reader["OrderId"]);
                        order.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
                        order.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        order.AddressId = Convert.ToInt32(reader["AddressId"] == DBNull.Value ? default : reader["AddressId"]);
                        order.TotalPrice = Convert.ToDouble(reader["TotalPrice"] == DBNull.Value ? default : reader["TotalPrice"]);
                        order.OrderQty = Convert.ToInt32(reader["OrderQty"] == DBNull.Value ? default : reader["OrderQty"]);
                        order.OrderDate = Convert.ToDateTime(reader["OrderDate"] == DBNull.Value ? default : reader["OrderDate"]);
                        order.BookName = Convert.ToString(reader["BookName"] == DBNull.Value ? default : reader["BookName"]);
                        order.AuthorName = Convert.ToString(reader["AuthorName"] == DBNull.Value ? default : reader["AuthorName"]);
                        order.BookImage = Convert.ToString(reader["BookImage"] == DBNull.Value ? default : reader["BookImage"]);
                        orderList.Add(order);
                    }
                    return orderList;
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

        public bool RemoveOrder(int orderId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spRemoveOrder", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OrderId", orderId);

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
    }
}
