using System.ComponentModel.DataAnnotations;

namespace LinkShortener.API.Controllers.Models 
{
    public class CreateLinkRequest
    {
        [Url(ErrorMessage = "Некорректный URL")]
        [Required(ErrorMessage = "URL обязателен для заполнения")]
        public required string OriginalUrl { get; set; }
    }
}
