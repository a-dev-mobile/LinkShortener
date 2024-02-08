using Microsoft.AspNetCore.Mvc;
using LinkShortener.Data;
using LinkShortener.Data.Models;
using System.Linq;

namespace LinkShortener.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinksController : ControllerBase
    {
        private readonly LinkShortenerDbContext _dbContext;

        public LinksController(LinkShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("create")]
        public IActionResult CreateShortLink([FromBody] string originalUrl)
        {
            var shortUrl = GenerateShortUrl(); 

            var link = new Link
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortUrl,
                DateCreated = DateTime.UtcNow
            };

            _dbContext.Links.Add(link);
            _dbContext.SaveChanges();

            return Ok(new { shortUrl });
        }

        [HttpGet("{shortUrl}")]
        public IActionResult RedirectToOriginalUrl(string shortUrl)
        {
            var link = _dbContext.Links.FirstOrDefault(l => l.ShortenedUrl == shortUrl);

            if (link == null)
                return NotFound();

            return Redirect(link.OriginalUrl);
        }

        private string GenerateShortUrl()
        {
            // Реализация генерации уникальной короткой ссылки
            return "someShortUrl";
        }
    }
}
