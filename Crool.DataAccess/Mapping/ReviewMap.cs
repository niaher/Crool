namespace Crool.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Crool.Domain.Entities;

    public class ReviewMap : EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            this.HasKey(e => e.Id);

            this.Property(e => e.From).HasColumnName("From").IsRequired();
            this.Property(e => e.To).HasColumnName("To").IsRequired();
            this.Property(e => e.StartLine).HasColumnName("StartLine").IsRequired();
            this.Property(e => e.EndLine).HasColumnName("EndLine").IsRequired();
            this.Property(e => e.Text).HasColumnName("Text").IsRequired();
            this.Property(e => e.Resolved).HasColumnName("Resolved").IsRequired();

            this.HasRequired(e => e.File)
                .WithMany()
                .HasForeignKey(e => e.File_Id);

            this.HasMany(e => e.Comments)
                .WithRequired()
                .HasForeignKey(c => c.Review_Id);
        }
    }
}
