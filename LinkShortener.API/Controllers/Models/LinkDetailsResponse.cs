namespace LinkShortener.API.Controllers.Models
{
    public class LinkDetailsResponse
    {
        public required string OriginalUrl { get; set; }
        public required string ShortUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
