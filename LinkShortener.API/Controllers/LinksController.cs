using Microsoft.AspNetCore.Mvc;
using LinkShortener.Data;
using LinkShortener.Data.Models;
using System.Linq;
using LinkShortener.Service.Interfaces;
using LinkShortener.API.Controllers.Models;

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
        [ProducesResponseType(typeof(CreateLinkResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateShortLink([FromBody] CreateLinkRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Возвращает ошибку, если URL невалиден
            }
            // Генерация короткой ссылки
            var shortUrl = await _linkShortenerService.GenerateShortLink(request.OriginalUrl);
            // Формирование полного URL
            var fullUrl = $"{Request.Scheme}://{Request.Host}/linkshortener/{shortUrl}";

            var response = new CreateLinkResponse
            {
                ShortUrl = shortUrl,
                FullUrl = fullUrl
            };

            // Возврат ответа с коротким и полным URL
            return Ok(response);
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLinks()
        {
            var links = await _linkShortenerService.GetAllLinksAsync();
            var response = links.Select(link =>
                    new LinkDetailsResponse { DateCreated = link.DateCreated, ShortUrl = $"{Request.Scheme}://{Request.Host}/linkshortener/{link.ShortenedUrl}", OriginalUrl = link.OriginalUrl }).ToList();

            return Ok(response);
        }


    }
}
