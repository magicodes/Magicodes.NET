using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Magicodes.CMS.DAL.Models.Mapping;

namespace Magicodes.CMS.DAL.Models
{
    public partial class CMSDbContext : DbContext
    {
        static CMSDbContext()
        {
            Database.SetInitializer<CMSDbContext>(null);
        }

        public CMSDbContext()
            : base("Name=magicodes_mvc")
        {
        }
        public DbSet<CMS_ClassType> CMS_ClassType { get; set; }
        public DbSet<CMS_Comment> CMS_Comment { get; set; }
        public DbSet<CMS_Content> CMS_Content { get; set; }
        public DbSet<CMS_ContentClass> CMS_ContentClass { get; set; }
        public DbSet<CMS_Photo> CMS_Photo { get; set; }
        public DbSet<CMS_PhotoAlbum> CMS_PhotoAlbum { get; set; }
        public DbSet<CMS_PhotoClass> CMS_PhotoClass { get; set; }
        public DbSet<CMS_Video> CMS_Video { get; set; }
        public DbSet<CMS_VideoAlbum> CMS_VideoAlbum { get; set; }
        public DbSet<CMS_VideoClass> CMS_VideoClass { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CMS_ClassTypeMap());
            modelBuilder.Configurations.Add(new CMS_CommentMap());
            modelBuilder.Configurations.Add(new CMS_ContentMap());
            modelBuilder.Configurations.Add(new CMS_ContentClassMap());
            modelBuilder.Configurations.Add(new CMS_PhotoMap());
            modelBuilder.Configurations.Add(new CMS_PhotoAlbumMap());
            modelBuilder.Configurations.Add(new CMS_PhotoClassMap());
            modelBuilder.Configurations.Add(new CMS_VideoMap());
            modelBuilder.Configurations.Add(new CMS_VideoAlbumMap());
            modelBuilder.Configurations.Add(new CMS_VideoClassMap());
        }
    }
}
