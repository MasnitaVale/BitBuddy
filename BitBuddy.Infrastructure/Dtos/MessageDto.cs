namespace BitBuddy.Core.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserId { get; set; }
        public DateTime Date { get; set; }
        public string? Text { get; set; }
        public string? PicturePath { get; set; }
        public int ChatId { get; set; }
    }
}
