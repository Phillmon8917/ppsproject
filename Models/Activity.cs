namespace PPSProject.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string? Type { get; set; }   // Commit, PR, Issue …
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public DateTime DateUtc { get; set; }

        public bool IsReadLater { get; set; }
        public bool IsFavourite { get; set; }
    }
}
