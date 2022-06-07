using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.Province
{
    public class FilterProvinces : PaginateDTO
    {
        public string Name { get; set; }
    }
}
