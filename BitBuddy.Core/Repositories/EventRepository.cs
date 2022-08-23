using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Entities;
using BitBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EventRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Event> CreateEvent(string userId, Event createdEvent)
        {
            createdEvent.OwnerId = userId;
            await _dbContext.Events.AddAsync(createdEvent);
            await _dbContext.SaveChangesAsync();
            await _dbContext.EventUsersRegistration.AddAsync(new EventUserRegistration { EventId = createdEvent.Id, UserId = userId });
            await _dbContext.SaveChangesAsync();

            return createdEvent;
        }

        public async Task<List<Event>> GetCreatedEvents(string userId)
        {
            return await _dbContext.Events.Where(e => e.OwnerId == userId).OrderByDescending(x => x.EventDate).ToListAsync();
        }

        public async Task<List<Event>> GetUpcomingEvents(string userId)
        {
            return await _dbContext.EventUsersRegistration
                .Include(x => x.Event).ThenInclude(e => e.EventInterests).ThenInclude(ei => ei.Interest)
                .Where(x => x.UserId == userId && x.Event.EventDate >= DateTime.UtcNow)
                .Select(x => x.Event)
                .OrderByDescending(x => x.EventDate)
                .ToListAsync();
        }

        public async Task<List<Event>> GetSuggestedEvents(string userId, int[] interestIds) //string userId
        {
            //return await _dbContext.Events
            //    .Include(e => e.EventInterests).ThenInclude(ei => ei.Interest)
            //    .OrderByDescending(x => x.EventDate)
            //    .ToListAsync();

            var registeredEventsIds = await _dbContext.EventUsersRegistration
                .Where(x => x.UserId == userId)
                .Select(x => x.EventId)
                .ToListAsync();

            var suggestedEvents = await _dbContext.Events
                .Include(e => e.EventInterests).ThenInclude(ei => ei.Interest)
                .Where(x => x.EventDate >= DateTime.UtcNow)
                .OrderByDescending(x => x.EventDate)
                .ToListAsync();

            var filteredSuggestions = suggestedEvents.Where(e =>
                interestIds.Any(id => e.EventInterests.Any(ei => ei.InterestId == id)) &&
                !registeredEventsIds.Any(x => x == e.Id));

            return filteredSuggestions.ToList();
        }

        public async Task<EventUserRegistration> RegisterUserToEvent(string userId, int eventId)
        {
            var newRegistration = new EventUserRegistration();
            newRegistration.UserId = userId;
            newRegistration.EventId = eventId;

            await _dbContext.EventUsersRegistration.AddAsync(newRegistration);
            await _dbContext.SaveChangesAsync();

            return newRegistration;
        }
    }
}
