using Microsoft.AspNetCore.Mvc;
using LinkShortener.Data;
using LinkShortener.Data.Models;
using System.Linq;
using LinkShortener.Service.Interfaces;

namespace LinkShortener.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class LinksController : ControllerBase
    {
        private readonly ILinkShortenerService _linkShortenerService;

        public LinksController(ILinkShortenerService linkShortenerService)
        {
            _linkShortenerService = linkShortenerService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateShortLink([FromBody] string originalUrl)
        {
            var shortUrl = await _linkShortenerService.GenerateShortLink(originalUrl);
            return Ok(new { shortUrl });
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl)
        {
            var link = await _linkShortenerService.GetLinkByShortUrlAsync(shortUrl);
            if (link == null)
            {
                return NotFound();
            }

            return Redirect(link.OriginalUrl);
        }


    }
}
