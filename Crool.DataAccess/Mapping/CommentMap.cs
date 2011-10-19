namespace Crool.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Crool.Domain.Entities;

    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            this.HasKey(e => e.Id);

            this.Property(e => e.PostedBy.Name).HasColumnName("PostedBy").IsRequired();
            this.Property(e => e.PostedOn).HasColumnName("PostedOn").IsRequired();
            this.Property(e => e.Text).HasColumnName("Text").IsRequired();
        }
    }
}
