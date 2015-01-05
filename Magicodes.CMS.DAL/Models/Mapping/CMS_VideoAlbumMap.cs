using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_VideoAlbumMap : EntityTypeConfiguration<CMS_VideoAlbum>
    {
        public CMS_VideoAlbumMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AlbumName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CoverVideo)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CMS_VideoAlbum");
        }
    }
}
