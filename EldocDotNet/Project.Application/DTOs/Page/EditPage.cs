using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.Page
{
    public class EditPage : CreatePage
    {
        [Required]
        public int Id { get; set; }
    }
}
