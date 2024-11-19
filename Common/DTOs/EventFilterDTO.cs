namespace Common.DTOs
{
    public class EventFilterDTO
    {
        public DateTime? Date { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}