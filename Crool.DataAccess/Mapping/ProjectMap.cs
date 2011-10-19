namespace Crool.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Crool.Domain.Entities;

    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            this.HasKey(e => e.Id);

            this.Property(e => e.Name).HasColumnName("Name").IsRequired();

            this.HasMany(e => e.Files)
                .WithRequired(f => f.Project)
                .HasForeignKey(f => f.Project_Id);
        }
    }
}
