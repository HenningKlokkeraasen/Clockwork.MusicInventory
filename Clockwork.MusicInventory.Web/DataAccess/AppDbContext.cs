using System.Data.Entity;
using Clockwork.MusicInventory.Web.Models;

namespace Clockwork.MusicInventory.Web.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public AppDbContext() : base("Clockwork.MusicInventory.Db") { }
    }
}
