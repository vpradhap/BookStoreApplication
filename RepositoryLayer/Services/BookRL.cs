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
    public class BookRL : IBookRL
    {
        public IConfiguration configuration { get; }
        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(BookModel book)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spAddbook", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookName", book.BookName);
                command.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                command.Parameters.AddWithValue("@Rating", book.Rating);
                command.Parameters.AddWithValue("@ReviewerCount", book.ReviewerCount);
                command.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                command.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice); 
                command.Parameters.AddWithValue("@BookDetail", book.BookDetail);
                command.Parameters.AddWithValue("@BookImage", book.BookImage);
                command.Parameters.AddWithValue("@BookQuantity", book.BookQuantity);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return book;
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

        public BookModel UpdateBook(BookModel book)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spUpdateBook", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", book.BookId);
                command.Parameters.AddWithValue("@BookName", book.BookName);
                command.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                command.Parameters.AddWithValue("@Rating", book.Rating);
                command.Parameters.AddWithValue("@ReviewerCount", book.ReviewerCount);
                command.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                command.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                command.Parameters.AddWithValue("@BookDetail", book.BookDetail);
                command.Parameters.AddWithValue("@BookImage", book.BookImage);
                command.Parameters.AddWithValue("@BookQuantity", book.BookQuantity);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return book;
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
        public bool DeleteBook(int bookId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                SqlCommand command = new SqlCommand("spDeleteBook", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", bookId);

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

        public List<BookModel> GetAllBooks()
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                List<BookModel> bookList = new List<BookModel>();
                SqlCommand command = new SqlCommand("spGetAllBooks", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BookModel book = new BookModel();
                        BookModel temp = GetBookrDetails(book, reader);
                        bookList.Add(temp);
                    }
                    return bookList;
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
        public BookModel GetBookById(int bookId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                BookModel book = new BookModel();
                SqlCommand command = new SqlCommand("spGetBookById", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", bookId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book = GetBookrDetails(book, reader);
                    }
                    return book;
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
        public static BookModel GetBookrDetails(BookModel book, SqlDataReader reader)
        {
            book.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
            book.BookName = Convert.ToString(reader["BookName"] == DBNull.Value ? default : reader["BookName"]);
            book.AuthorName = Convert.ToString(reader["AuthorName"] == DBNull.Value ? default : reader["AuthorName"]);
            book.Rating = Convert.ToDouble(reader["Rating"] == DBNull.Value ? default : reader["Rating"]);
            book.ReviewerCount = Convert.ToInt32(reader["ReviewerCount"] == DBNull.Value ? default : reader["ReviewerCount"]);
            book.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"] == DBNull.Value ? default : reader["DiscountPrice"]);
            book.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"] == DBNull.Value ? default : reader["OriginalPrice"]);
            book.BookDetail = Convert.ToString(reader["BookDetail"] == DBNull.Value ? default : reader["BookDetail"]);
            book.BookImage = Convert.ToString(reader["BookImage"] == DBNull.Value ? default : reader["BookImage"]);
            book.BookQuantity = Convert.ToInt32(reader["BookQuantity"] == DBNull.Value ? default : reader["BookQuantity"]);
            
            return book;
        }
    }
}
