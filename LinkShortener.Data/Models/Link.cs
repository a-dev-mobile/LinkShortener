namespace LinkShortener.Data.Models
{
    public class Link
    {
        public int Id { get; set; } // Первичный ключ
        public string OriginalUrl { get; set; } // Исходный URL
        public string ShortenedUrl { get; set; } // Сокращенный URL
        public DateTime DateCreated { get; set; } // Дата создания записи

        public Link()
        {
            OriginalUrl = string.Empty; 
            ShortenedUrl = string.Empty; 
            DateCreated = DateTime.Now;
        }
    }

}
