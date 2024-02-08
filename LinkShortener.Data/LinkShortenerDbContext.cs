using LinkShortener.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data
{
    public class LinkShortenerDbContext : DbContext
    {
        public LinkShortenerDbContext(DbContextOptions<LinkShortenerDbContext> options)
            : base(options)
        {
        }
        public DbSet<Link> Links { get; set; }

    }
}
