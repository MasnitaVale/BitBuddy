
using BitBuddy.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BitBuddy.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Interest
            modelBuilder.Entity<Interest>().HasKey(x => x.Id);

            modelBuilder.Entity<UserInterest>().HasKey(x => new { x.UserId, x.InterestId });
            modelBuilder.Entity<UserInterest>().HasOne(x => x.Interest).WithMany(i => i.UserInterests).HasForeignKey(x => x.InterestId);
            modelBuilder.Entity<UserInterest>().HasOne(x => x.User).WithMany(u => u.UserInterests).HasForeignKey(x => x.UserId);

            // Event
            modelBuilder.Entity<Event>().HasKey(x => x.Id);
            modelBuilder.Entity<Event>().HasOne(e => e.Owner).WithMany(u => u.CreatedEvents).HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EventInterest>().HasKey(x => new { x.InterestId, x.EventId });
            modelBuilder.Entity<EventInterest>().HasOne(x => x.Event).WithMany(i => i.EventInterests).HasForeignKey(x => x.EventId);
            modelBuilder.Entity<EventInterest>().HasOne(x => x.Interest).WithMany(i => i.EventInterests).HasForeignKey(x => x.InterestId);

            modelBuilder.Entity<EventUserRegistration>().HasKey(x => new { x.UserId, x.EventId });
            modelBuilder.Entity<EventUserRegistration>().HasOne(x => x.User).WithMany(u => u.EventUserRegistrations).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<EventUserRegistration>().HasOne(x => x.Event).WithMany(u => u.EventUserRegistrations).HasForeignKey(x => x.EventId);

            // User Friend
            modelBuilder.Entity<UserFriend>().HasKey(x => new { x.User1Id, x.User2Id });
            modelBuilder.Entity<UserFriend>().HasOne(x => x.User1).WithMany(u => u.UserFriends).HasForeignKey(x => x.User1Id).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserFriend>().HasOne(x => x.User2).WithMany(u => u.UserFriendsOf).HasForeignKey(x => x.User2Id).OnDelete(DeleteBehavior.NoAction);

            // User Matchings
            modelBuilder.Entity<UserMatching>().HasKey(x => new { x.User1Id, x.User2Id });
            modelBuilder.Entity<UserMatching>().HasOne(x => x.User1).WithMany(u => u.UserMatchings).HasForeignKey(x => x.User1Id).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserMatching>().HasOne(x => x.User2).WithMany(u => u.UserMatchingsOf).HasForeignKey(x => x.User2Id).OnDelete(DeleteBehavior.NoAction);

            // Chats + Messages + UserChats
            modelBuilder.Entity<Chat>().HasKey(x => x.Id);

            modelBuilder.Entity<Message>().HasKey(x => x.Id);
            modelBuilder.Entity<Message>().HasOne(x => x.Chat).WithMany(c => c.Messages).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserChat>().HasKey(x => new { x.UserId, x.ChatId });
            modelBuilder.Entity<UserChat>().HasOne(x => x.User).WithMany(c => c.UserChats).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserChat>().HasOne(x => x.Chat).WithMany(c => c.UserChats).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.NoAction);

            // Reports
            modelBuilder.Entity<Report>().HasKey(x => x.id);
            modelBuilder.Entity<Report>().HasOne(x => x.User).WithMany();

            SeedData(modelBuilder);
        }

        #region DbSets
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventInterest> EventInterests { get; set; }
        public DbSet<EventUserRegistration> EventUsersRegistration { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<UserMatching> UserMatchings { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Report> Reports { get; set; }
        #endregion



        #region SeedMethods

        private void SeedData(ModelBuilder modelBuilder)
        {
            SeedInterests(modelBuilder);
            SeedUserRoles(modelBuilder);
        }

        private void SeedInterests(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interest>()
                .HasData(
                new Interest { Id = 1, Name = "Cooking" },
                new Interest { Id = 2, Name = "Drawing" },
                new Interest { Id = 3, Name = "Hiking" });
        }


        private void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasData(
                new UserRole
                {
                    Name = AppUserRoles.Admin,
                    NormalizedName = AppUserRoles.Admin
                },
                new UserRole
                {
                    Name = AppUserRoles.User,
                    NormalizedName = AppUserRoles.User
                });
        }
        #endregion
    }
}