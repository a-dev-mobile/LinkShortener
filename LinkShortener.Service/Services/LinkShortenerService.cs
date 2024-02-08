using LinkShortener.Data.Interfaces;
using LinkShortener.Data.Models;
using LinkShortener.Service.Interfaces;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class LinkShortenerService : ILinkShortenerService
{
    private readonly ILinkRepository _linkRepository;

    public LinkShortenerService(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task<string> GenerateShortLink(string originalUrl)
    {
        // Генерация уникальной короткой ссылки
        string shortUrl = GenerateUniqueKey();

        // Проверка уникальности короткой ссылки в базе данных
        var existingLink = await _linkRepository.GetLinkByShortUrlAsync(shortUrl);
        while (existingLink != null)
        {
            shortUrl = GenerateUniqueKey(); // Генерация новой, если такая уже есть
            existingLink = await _linkRepository.GetLinkByShortUrlAsync(shortUrl);
        }

        // Сохранение в базу данных
        await _linkRepository.AddLinkAsync(new Link { OriginalUrl = originalUrl, ShortenedUrl = shortUrl });

        return shortUrl;
    }

    public async Task<Link?> GetLinkByShortUrlAsync(string shortUrl)
    {
        return await _linkRepository.GetLinkByShortUrlAsync(shortUrl);
    }

    private string GenerateUniqueKey()
    {
        // Генерация уникального ключа с использованием RandomNumberGenerator
        byte[] randomBytes = new byte[4]; // 4 байта дадут 8 символов в hex
        RandomNumberGenerator.Fill(randomBytes);

        // Преобразование в строку hex
        string uniqueKey = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
        return uniqueKey;
    }
}
