using LinkShortener.Data.Models;

namespace LinkShortener.Data.Interfaces
{
    public interface ILinkRepository
    {
        Task<Link> AddLinkAsync(Link link);
        Task<List<Link>> GetAllLinksAsync();
        Task<Link?> GetLinkByShortUrlAsync(string shortUrl);
      
    }
}
