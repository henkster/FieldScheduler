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
            modelBuilder.Entity<Field>().Ignore(f => f.Size);

            modelBuilder.Entity<Field>()
                .HasMany(f => f.FieldsProhibitingThis)
                .WithMany(f => f.FieldsProhibitedByThis)
                .Map(x =>
                         {
                             x.MapLeftKey("Field_Id");
                             x.MapRightKey("Conflict_Field_Id");
                             x.ToTable("FieldsConflictFields");
                         });
            
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
        public DbSet<Division> Divisions { get; set; }
        public DbSet<GameChangeHistory> GameChangeHistories { get; set; }
    }
}