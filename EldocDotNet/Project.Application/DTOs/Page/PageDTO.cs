using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.Page
{
    public class PageDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Uri { get; set; }
    }
}
