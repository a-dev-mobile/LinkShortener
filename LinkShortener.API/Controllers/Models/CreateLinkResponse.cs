namespace LinkShortener.API.Controllers.Models
{
    public class CreateLinkResponse
    {
        public required string ShortUrl { get; set; }
        public required string FullUrl { get; set; }
    }
}
