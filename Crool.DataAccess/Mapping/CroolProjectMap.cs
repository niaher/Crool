namespace Crool.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Crool.Domain.Entities;

    public class CroolProjectMap : EntityTypeConfiguration<CroolProject>
    {
        public CroolProjectMap()
        {
            this.HasKey(e => e.Id);

            this.Property(e => e.Name).IsRequired();

            this.HasMany(e => e.Projects)
                .WithRequired()
                .HasForeignKey(p => p.CroolProject_Id);
        }
    }
}
