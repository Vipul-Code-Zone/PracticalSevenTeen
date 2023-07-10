using System.ComponentModel.DataAnnotations;

namespace PracSevenTeen.Models.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^([a-zA-z ]){1,50}", ErrorMessage = "Name only contains the Alphabets")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"1[0-2]|[1-9]", ErrorMessage = "Standard only between 1 to 12")]
        public int Standard { get; set; }

        [RegularExpression(@"^[1-9][0-9]?$", ErrorMessage = "Age can only between the 1 to 99")]
        public int Age { get; set; }
    }
}
