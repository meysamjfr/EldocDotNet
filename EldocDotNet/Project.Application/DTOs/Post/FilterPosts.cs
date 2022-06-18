using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.Post
{
    public class FilterPosts : PaginateDTO
    {
        public string Search { get; set; }
        public int? CategoryId { get; set; }
    }    
}
