namespace Crool.AddIn.Business.DataAcess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Crool.AddIn.Business.Entities;

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
