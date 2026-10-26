using System.ComponentModel.DataAnnotations;

namespace messanger.Models
{
    public class UserDTO // класс для пользователей при взаимодействии с сервером
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }


}