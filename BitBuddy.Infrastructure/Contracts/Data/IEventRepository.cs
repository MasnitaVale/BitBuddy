using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Contracts.Data
{
    public interface IEventRepository
    {
        Task<Event> CreateEvent(string useriId, Event createdEvent);
        Task<List<Event>> GetSuggestedEvents(string userId, int[] interestIds); 
        Task<List<Event>> GetUpcomingEvents(string userId);
        Task<List<Event>> GetCreatedEvents(string userId);
        Task<EventUserRegistration> RegisterUserToEvent(string userId, int EventId);
    }
}
