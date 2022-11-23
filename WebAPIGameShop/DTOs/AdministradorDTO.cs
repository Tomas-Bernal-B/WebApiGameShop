using System.ComponentModel.DataAnnotations;

namespace WebAPIGameShop.DTOs
{
    public class AdministradorDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
