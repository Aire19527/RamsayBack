using System.ComponentModel.DataAnnotations;

namespace Ramsay.Domain.DTOs.Student
{
    public class AddStudentDto
    {
        [Required(ErrorMessage = "the Username of Student is mandatory")]
        [MaxLength(20, ErrorMessage = "the maximum number of characters is: {1}")]

        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "the FirstName of Student is mandatory")]
        [MaxLength(20, ErrorMessage = "the maximum number of characters is: {1}")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "the LastName of Student is mandatory")]
        [MaxLength(20, ErrorMessage = "the maximum number of characters is: {1}")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "the Age of Student is mandatory")]
        public int Age { get; set; }

        [Required(ErrorMessage = "the Career of Student is mandatory")]
        [MaxLength(50, ErrorMessage = "the maximum number of characters is: {1}")]
        public string Career { get; set; } = null!;
    }
}
