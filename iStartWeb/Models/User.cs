using System.ComponentModel.DataAnnotations;

namespace iStartWeb.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(75)]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabet characters are allowed.")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Gender { get; set; }
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,12}$", ErrorMessage = "Must have 8 to 12 charecters and contains 1 uppercase, 1 special character, 1 numeric.")]
        public string Password { get; set; }
    }
}
