using Microsoft.AspNetCore.Http;

namespace Project.Application.DTOs.Post
{
    public class UpsertPost
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int PostCategoryId { get; set; }
        public string PostCategory { get; set; }
#nullable enable
        public IFormFile? Image { get; set; }
#nullable disable
    }
}
