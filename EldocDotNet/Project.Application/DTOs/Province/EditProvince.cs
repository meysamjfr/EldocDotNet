using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.Province
{
    public class EditProvince : CreateProvince
    {
        [Required]
        public int Id { get; set; }
    }
}
