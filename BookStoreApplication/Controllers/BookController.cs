using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApplication.Controllers
{
    [Route("")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Addbook")]
        public IActionResult AddBook(BookModel book)
        {
            try
            {
                var result = bookBL.AddBook(book);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Book added", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to Add book" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        [Route("Updatebook")]
        public IActionResult UpdateBook(BookModel book)
        {
            try
            {
                var result = bookBL.UpdateBook(book);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Book updated", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to update" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("Deletebook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                var result = bookBL.DeleteBook(bookId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Book deleted" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book does not exist" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Route("Getallbooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = bookBL.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Your Books", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "something went wrong" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetbookbyId")]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var result = bookBL.GetBookById(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Your Book", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book does not exist" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
