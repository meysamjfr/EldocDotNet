using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.City
{
    public class FilterCites : PaginateDTO
    {
        public string Name { get; set; }
        public int? ProvinceId { get; set; }
    }
}
