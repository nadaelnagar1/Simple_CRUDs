namespace CRUD_DAL.Database.ApplicationDbContext
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<UserLanguage>()
            //    .HasOne(e => e.User)
            //    .WithMany(p => p.userLanguages)
            //    .HasForeignKey(e => e.UserId);
        }

        public virtual DbSet<Student> Students { get; set; }


    }
}
