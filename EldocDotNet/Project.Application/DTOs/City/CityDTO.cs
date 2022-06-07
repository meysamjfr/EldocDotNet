using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.City
{
    public class CityDTO : BaseDTO
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public string Province { get; set; }
    }
}
