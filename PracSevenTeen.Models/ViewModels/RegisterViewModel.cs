using System.ComponentModel.DataAnnotations;

namespace PracticalSevenTeen.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }


        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Phone Number is Only Contains the Number")]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Both Password Must be same")]
        public string ConfirmPassword { get; set; }
    }
}
