using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.FAQ
{
    public class FAQDTO : BaseDTO
    {
        public string Question { get; set; }
        public string Content { get; set; }
    }
}
