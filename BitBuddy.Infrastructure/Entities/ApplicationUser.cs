using Microsoft.AspNetCore.Identity;

namespace BitBuddy.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? NickName { get; set; }
        public string? Studies { get; set; }
        public string? Location { get; set; }
        public string? Introduction { get; set; }
        public string? ProfilePicturePath { get; set; }
        public int? Age { get; set; }
        public ICollection<UserInterest> UserInterests { get; set; }
        public ICollection<Event> CreatedEvents { get; set; }
        public ICollection<EventUserRegistration> EventUserRegistrations { get; set; }
        public ICollection<UserFriend> UserFriends { get; set; }
        public ICollection<UserFriend> UserFriendsOf { get; set; }
        public ICollection<UserMatching> UserMatchings { get; set; }
        public ICollection<UserMatching> UserMatchingsOf { get; set; }
        public ICollection<UserChat> UserChats { get; set; }
    }
}
