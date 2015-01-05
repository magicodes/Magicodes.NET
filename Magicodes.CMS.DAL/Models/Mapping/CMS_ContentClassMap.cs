using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_ContentClassMap : EntityTypeConfiguration<CMS_ContentClass>
    {
        public CMS_ContentClassMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(200);

            this.Property(t => t.Keywords)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
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

            // Table & Column Mappings
            this.ToTable("CMS_ContentClass");
        }
    }
}
