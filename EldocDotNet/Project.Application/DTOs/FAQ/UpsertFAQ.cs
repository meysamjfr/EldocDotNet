namespace Project.Application.DTOs.FAQ
{
    public class UpsertFAQ
    {
        public int? Id { get; set; }
        public string Question { get; set; }
        public string Content { get; set; }
    }
}
