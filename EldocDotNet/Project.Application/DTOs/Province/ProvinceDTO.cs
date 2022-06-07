using Project.Application.DTOs.Base;
using Project.Application.DTOs.City;

namespace Project.Application.DTOs.Province
{
    public class ProvinceDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<CityDTO> Cities { get; set; }
    }
}
