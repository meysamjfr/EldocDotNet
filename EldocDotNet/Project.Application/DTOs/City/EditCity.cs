using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.City
{
    public class EditCity : CreateCity
    {
        [Required]
        public int Id { get; set; }
    }
}
