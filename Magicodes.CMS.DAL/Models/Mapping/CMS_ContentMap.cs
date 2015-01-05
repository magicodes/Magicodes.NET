using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_ContentMap : EntityTypeConfiguration<CMS_Content>
    {
        public CMS_ContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SubTitle)
                .HasMaxLength(255);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(100);

            this.Property(t => t.ThumbImageUrl)
                .HasMaxLength(200);

            this.Property(t => t.NormalImageUrl)
                .HasMaxLength(200);

            this.Property(t => t.LinkUrl)
                .HasMaxLength(200);

            this.Property(t => t.Keywords)
                .HasMaxLength(50);

            this.Property(t => t.Attachment)
                .HasMaxLength(200);

            this.Property(t => t.Remary)
                .HasMaxLength(200);

            this.Property(t => t.BeFrom)
                .HasMaxLength(50);

            this.Property(t => t.FileName)
                .HasMaxLength(200);

            this.Property(t => t.Meta_Title)
                .HasMaxLength(1000);

            this.Property(t => t.Meta_Description)
                .HasMaxLength(1000);

            this.Property(t => t.Meta_Keywords)
                .HasMaxLength(1000);

            this.Property(t => t.SeoUrl)
                .HasMaxLength(300);

            this.Property(t => t.SeoImageAlt)
                .HasMaxLength(300);

            this.Property(t => t.SeoImageTitle)
                .HasMaxLength(300);

            this.Property(t => t.StaticUrl)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CMS_Content");
        }
    }
}
