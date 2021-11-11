using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class User
    {
        [Key, Required]
        public string UserName { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public int SecurityLevel { get; set; }
        [Required]
        public string Password { get; set; }
    }
}