using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.Post
{
    public class PostDTO : BaseDTO
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int PostCategoryId { get; set; }
        public string PostCategory { get; set; }
    }
}
