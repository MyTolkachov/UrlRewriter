using System.Data.Entity;
using URL.Rewriter.DAL.Models;

namespace URL.Rewriter.DAL
{
    public class UrlsDbContext : DbContext
    {
        public UrlsDbContext() : base("UrlDb")
        {
        }

        public DbSet<UrlEntity> Urls { get; set; }
    }
}
