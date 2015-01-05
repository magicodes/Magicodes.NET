using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_PhotoAlbumMap : EntityTypeConfiguration<CMS_PhotoAlbum>
    {
        public CMS_PhotoAlbumMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AlbumName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CMS_PhotoAlbum");
        }
    }
}
