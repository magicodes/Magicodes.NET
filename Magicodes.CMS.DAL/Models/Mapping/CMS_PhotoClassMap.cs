using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_PhotoClassMap : EntityTypeConfiguration<CMS_PhotoClass>
    {
        public CMS_PhotoClassMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ClassName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CMS_PhotoClass");
        }
    }
}
