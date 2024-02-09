using LinkShortener.Data.Models;

namespace LinkShortener.Service.Interfaces
{
    public interface ILinkShortenerService
    {
        Task<string> GenerateShortLink(string originalUrl);
        Task<Link?> GetLinkByShortUrlAsync(string shortUrl);
        Task<List<Link>> GetAllLinksAsync();
    }
}
