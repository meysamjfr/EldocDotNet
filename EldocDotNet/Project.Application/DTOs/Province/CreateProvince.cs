using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.Province
{
    public class CreateProvince
    {
        [Required]
        public string Name { get; set; }
    }
}
