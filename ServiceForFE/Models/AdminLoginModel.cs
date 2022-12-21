using System.ComponentModel.DataAnnotations;

namespace AdminWeb.Models
{
    public class AdminLoginModel
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
