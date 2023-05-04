using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Entity.Models
{
    public class StudentEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Username { get; set; } = null!;
        [MaxLength(20)]
        public string FirstName { get; set; } = null!;
        [MaxLength(200)]
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        [MaxLength(50)]
        public string Career { get; set; } = null!;
    }
}
