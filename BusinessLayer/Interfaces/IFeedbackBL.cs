using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public string AddFeedback(AddFeedbackModel feedback, int userId);
        public List<FeedbackModel> GetFeedback(int bookId);
    }
}
