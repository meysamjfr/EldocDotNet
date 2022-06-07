using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.City
{
    public class CreateCity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int ProvinceId { get; set; }
    }
}
