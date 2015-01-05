using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_ClassTypeMap : EntityTypeConfiguration<CMS_ClassType>
    {
        public CMS_ClassTypeMap()
        {
            // Properties
            this.Property(t => t.ClassTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CMS_ClassType");
        }
    }
}
