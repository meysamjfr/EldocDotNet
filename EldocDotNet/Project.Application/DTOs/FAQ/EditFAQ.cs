using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.FAQ
{
    public class EditFAQ : CreateFAQ
    {
        [Required]
        public int Id { get; set; }
    }
}
