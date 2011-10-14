namespace Crool.AddIn.Business.DataAcess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Crool.AddIn.Business.Entities;

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
