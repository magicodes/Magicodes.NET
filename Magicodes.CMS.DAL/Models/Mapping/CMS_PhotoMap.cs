using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_PhotoMap : EntityTypeConfiguration<CMS_Photo>
    {
        public CMS_PhotoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PhotoName)
                .HasMaxLength(200);

            this.Property(t => t.ImageUrl)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.ThumbImageUrl)
                .HasMaxLength(200);

            this.Property(t => t.NormalImageUrl)
                .HasMaxLength(200);

            this.Property(t => t.Tags)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CMS_Photo");
        }
    }
}
