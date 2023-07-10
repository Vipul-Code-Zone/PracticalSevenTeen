using System.ComponentModel.DataAnnotations;

namespace PracticalSevenTeen.ViewModels
{
    public class LoginViewModel
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
