namespace ELECTIVE_H1_BSIT_32E3_MatigaXyrylleClaire.Models
{
    public class Resolution
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateResolutionDto
    {
        public string Title { get; set; } = string.Empty;
    }

    public class UpdateResolutionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }

    public class ErrorResponse
    {
        public string Error { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<string> Details { get; set; } = new();
    }

    public class ResolutionListResponse
    {
        public List<Resolution> Items { get; set; } = new();
    }
}
