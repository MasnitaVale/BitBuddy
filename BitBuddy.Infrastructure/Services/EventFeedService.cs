using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Contracts.Domain;
using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Services
{
    public class EventFeedService : IEventFeedService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IInterestRepository _interestRepository;

        public EventFeedService(IEventRepository eventRepository, IUserRepository userRepository, IInterestRepository interestRepository)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _interestRepository = interestRepository;
        }

        public async Task<List<Event>> GetSuggestedEvents(string userId)
        {
            var userInterestIds = await _interestRepository.GetUserInterestsById(userId);
            return await _eventRepository.GetSuggestedEvents(userId, userInterestIds);
        }

        public async Task<string> GetUserIdByUsername(string username)
        {
            return await _userRepository.GetUserIdByUsername(username);
        }

        public async Task<Event> AddEvent(string userId, Event newEvent)
        {
            return await _eventRepository.CreateEvent(userId, newEvent);
        }
        public async Task<List<Interest>> GetInterests()
        {
            return await _interestRepository.GetInterests();
        }
        public async Task<EventUserRegistration> RegisterUserToEvent(string userId, int EventId)
        {
            return await _eventRepository.RegisterUserToEvent(userId, EventId);
        }

        public async Task<List<Event>> GetUpcomingEvents(string userId)
        {
            return await _eventRepository.GetUpcomingEvents(userId);
        }

        public async Task<List<Event>> GetCreatedEvents(string userId)
        {
            return await _eventRepository.GetCreatedEvents(userId);
        }
    }
}
