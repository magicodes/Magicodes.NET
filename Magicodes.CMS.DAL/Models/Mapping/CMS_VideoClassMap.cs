using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_VideoClassMap : EntityTypeConfiguration<CMS_VideoClass>
    {
        public CMS_VideoClassMap()
        {
            // Properties
            this.Property(t => t.VideoClassName)
                .IsRequired()
                .HasMaxLength(100);


            // Table & Column Mappings
            this.ToTable("CMS_VideoClass");
            this.Property(t => t.VideoClassName).HasColumnName("VideoClassName");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
        }
    }
}
