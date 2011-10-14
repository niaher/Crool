namespace Crool.AddIn.Business.DataAcess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Crool.AddIn.Business.Entities;

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
