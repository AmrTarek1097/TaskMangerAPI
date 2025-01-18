namespace TaskManagerAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MemberTask> Tasks { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MemberTask>()
                .HasOne(t => t.TeamMember)
                .WithMany(m => m.Tasks)
                .HasForeignKey(t => t.TeamMemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
