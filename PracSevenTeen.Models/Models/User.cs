using System.ComponentModel.DataAnnotations;

namespace PracSevenTeen.Models.Models;
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "First name should be less than 50 character")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Last name should be less than 50 character")]
    public string LastName { get; set; }


    [Required]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    //[StringLength(10)]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Phone Must be Number")]
    public string MobileNumber { get; set; }

    [Required]
    [StringLength(50)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}
