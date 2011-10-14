namespace Crool.AddIn.Business.DataAcess
{
    using System.Data.Entity;
    using Entities;
    using System.Data.Entity.Infrastructure;
    using Crool.AddIn.Business.DataAcess.Mapping;

    public class CroolContext : DbContext
    {
        static CroolContext()
        {
            //Database.SetInitializer<CroolContext>(null);
        }

        public CroolContext(string connection) : base(connection)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<CroolProject> CroolProjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            modelBuilder.Configurations.Add(new ReviewMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new CroolProjectMap());
        }
    }
}
