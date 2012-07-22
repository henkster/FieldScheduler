using System.Data.Entity;
using Domain;

namespace Data
{
    public class Context : DbContext
    {
        static Context()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public Context() : base("Scheduler")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slot>().Ignore(s => s.EndTime);
            modelBuilder.Entity<Field>().Ignore(f => f.Size);
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Field> Fields { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClubTeam> ClubTeams { get; set; }
        public DbSet<ExternalTeam> ExternalTeams { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}