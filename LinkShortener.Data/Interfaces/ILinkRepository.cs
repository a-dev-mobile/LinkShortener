using LinkShortener.Data.Models;

namespace LinkShortener.Data.Interfaces
{
    public interface ILinkRepository
    {
        Task<Link> AddLinkAsync(Link link);
        Task<Link?> GetLinkByShortUrlAsync(string shortUrl);
      
    }
}
