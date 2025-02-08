using EventEaseApp.Models;

namespace EventEaseApp.Services
{
    public class EventService
    {
        public Task<List<Event>> GetEventsAsync()
        {
            // Mock data or fetch from API
            return Task.FromResult(new List<Event>
            {
                new Event { Id = 1, Name = "Corporate Seminar", Date = DateTime.Now, Location = "New York" },
                new Event { Id = 2, Name = "Social Gathering", Date = DateTime.Now.AddDays(7), Location = "Los Angeles" }
            });
        }
    }

}