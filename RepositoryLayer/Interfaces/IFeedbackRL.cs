using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRL
    {
        public string AddFeedback(AddFeedbackModel feedback, int userId);
        public List<FeedbackModel> GetFeedback(int bookId);
    }
}
