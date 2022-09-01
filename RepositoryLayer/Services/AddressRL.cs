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
    public class AddressRL : IAddressRL
    {
        public IConfiguration Configuration { get; }

        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddAddress(AddressModel address, int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddAddress", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Address", address.Address);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@State", address.State);
                command.Parameters.AddWithValue("@TypeId", address.Type);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return "Address added";
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
        public AddressModel UpdateAddress(AddressModel address, int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spUpdateAddress", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AddressId", address.AddressId);
                command.Parameters.AddWithValue("@Address", address.Address);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@State", address.State);
                command.Parameters.AddWithValue("@TypeId", address.Type);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return address;
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

        public List<AddressModel> GetAllAddress(int userId)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                List<AddressModel> addressList = new List<AddressModel>();
                SqlCommand command = new SqlCommand("spGetAllAddress", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AddressModel address = new AddressModel();
                        AddressModel temp = GetAddress(address, reader);
                        addressList.Add(temp);
                    }
                    return addressList;
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
        public static AddressModel GetAddress(AddressModel address, SqlDataReader rdr)
        {
            address.AddressId = Convert.ToInt32(rdr["Addressid"] == DBNull.Value ? default : rdr["Addressid"]);
            address.Address = Convert.ToString(rdr["Address"] == DBNull.Value ? default : rdr["Address"]);
            address.City = Convert.ToString(rdr["City"] == DBNull.Value ? default : rdr["City"]);
            address.State = Convert.ToString(rdr["State"] == DBNull.Value ? default : rdr["State"]);
            address.Type = Convert.ToInt32(rdr["TypeId"] == DBNull.Value ? default : rdr["TypeId"]);
            return address;
        }
    }
}
