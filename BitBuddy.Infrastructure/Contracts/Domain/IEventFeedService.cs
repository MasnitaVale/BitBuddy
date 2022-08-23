using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Contracts.Domain
{
    public interface IEventFeedService
    {
        Task<List<Event>> GetSuggestedEvents(string userId);
        Task<string> GetUserIdByUsername(string username);
        Task<Event> AddEvent(string userId, Event newEvent);
        Task<List<Interest>> GetInterests();
        Task<EventUserRegistration> RegisterUserToEvent(string userId, int EventId);
        Task<List<Event>> GetUpcomingEvents(string userId);
        Task<List<Event>> GetCreatedEvents(string userId);
    }
}
