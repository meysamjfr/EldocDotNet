using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.PostCategory
{
    public class PostCategoryDTO : BaseDTO
    {
        public string Title { get; set; }
        public int TotalPosts { get; set; }
    }
}
