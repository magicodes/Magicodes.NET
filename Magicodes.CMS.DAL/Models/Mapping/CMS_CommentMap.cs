using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Magicodes.CMS.DAL.Models.Mapping
{
    public class CMS_CommentMap : EntityTypeConfiguration<CMS_Comment>
    {
        public CMS_CommentMap()
        {
            // Properties
            this.Property(t => t.Description)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CMS_Comment");
        }
    }
}
