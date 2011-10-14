namespace Crool.AddIn.Business.DataAcess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Crool.AddIn.Business.Entities;

    public class FileMap : EntityTypeConfiguration<File>
    {
        public FileMap()
        {
            this.HasKey(e => e.Id);

            this.Property(e => e.FileName).HasColumnName("FileName").IsRequired();

            this.HasMany(e => e.Reviews)
                .WithRequired(r => r.File)
                .HasForeignKey(r => r.File_Id);
        }
    }
}
