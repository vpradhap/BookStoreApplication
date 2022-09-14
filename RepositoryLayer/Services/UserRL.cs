    using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public IConfiguration Configuration { get; }

        public UserRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public UserRegisterModel Register(UserRegisterModel user)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spRegister", connection);
                command.CommandType = CommandType.StoredProcedure;

                var encrypted = EncryptPasswordBase64(user.Password);

                command.Parameters.AddWithValue("@FullName", user.FullName);
                command.Parameters.AddWithValue("@EmailId", user.EmailId);
                command.Parameters.AddWithValue("@Password", encrypted);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result == 1)
                {
                    return user;
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

        public LoginResponseModel Login(LoginModel user)
        {
            LoginResponseModel loginResponse = new LoginResponseModel();
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spLogin", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmailId", user.EmailId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        loginResponse.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        var password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                        loginResponse.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);
                        loginResponse.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                        loginResponse.MobileNumber = Convert.ToInt64(reader["MobileNumber"] == DBNull.Value ? default : reader["MobileNumber"]);
                        var decrypted = DecryptPasswordBase64(password);
                        if (decrypted == user.Password)
                        {
                            loginResponse.token = JwtMethod(loginResponse.EmailId, loginResponse.UserId);
                            return loginResponse;
                        }
                    }
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
            return default;
        }

        public string JwtMethod(string emailID, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration[("Jwt:Secretkey")]));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Email, emailID),
                        new Claim("UserId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string ForgotPassword(string emailId)
        {

            using (SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]))
            {
                try
                {
                    SqlCommand command = new SqlCommand("spForget", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailId", emailId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var userId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                            var token = JwtMethod(emailId, userId);
                            MSMQ_Model msmq_Model = new MSMQ_Model();
                            msmq_Model.sendData2Queue($"http://localhost:4200/resetpassword/{token}");
                            return $"http://localhost:4200/resetpassword/{token}";
                        }
                    }
                    else
                    { return null; }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return default;
        }

        public static string EncryptPasswordBase64(string password)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string DecryptPasswordBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string ResetPassword(ResetPasswordModel user)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spResetPassword", connection);
                command.CommandType = CommandType.StoredProcedure;

                var encryptedPassword = EncryptPasswordBase64(user.Password);

                command.Parameters.AddWithValue("@EmailId", user.EmailId);
                command.Parameters.AddWithValue("@Password", encryptedPassword);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return "Password Updated";
                }
                else
                {
                    return "Failed to update password";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
