using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_VideoMap : EntityTypeConfiguration<CMS_Video>
    {
        public CMS_VideoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(100);

            this.Property(t => t.ThumbImageUrl)
                .HasMaxLength(100);

            this.Property(t => t.NormalImageUrl)
                .HasMaxLength(100);

            this.Property(t => t.Tags)
                .HasMaxLength(100);

            this.Property(t => t.VideoFormat)
                .HasMaxLength(50);

            this.Property(t => t.Domain)
                .HasMaxLength(50);

            this.Property(t => t.Attachment)
                .HasMaxLength(100);

            this.Property(t => t.Remark)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("CMS_Video");
        }
    }
}
