using LinkShortener.Data.Interfaces;
using LinkShortener.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LinkShortener.Data.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly LinkShortenerDbContext _context;

        public LinkRepository(LinkShortenerDbContext context)
        {
            _context = context;
        }

        public async Task<Link> AddLinkAsync(Link link)
        {
            _context.Links.Add(link);
            await _context.SaveChangesAsync();
            return link;
        }

        public async Task<Link?> GetLinkByShortUrlAsync(string shortUrl)
        {
            return await _context.Links.FirstOrDefaultAsync(l => l.ShortenedUrl == shortUrl);
        }
    }
}
