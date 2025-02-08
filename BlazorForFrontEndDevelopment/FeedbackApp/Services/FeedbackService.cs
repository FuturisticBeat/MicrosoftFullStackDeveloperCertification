using FeedbackApp.Models;

namespace FeedbackApp.Services
{
    public class FeedbackService
    {
        private readonly List<Feedback> _feedbackList = [];
        
        public void AddFeedback(Feedback feedback)
        {
            _feedbackList.Add(feedback);
        }
        
        public IEnumerable<Feedback> GetFeedbackList() => _feedbackList;
    }
}