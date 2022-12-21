using System.ComponentModel.DataAnnotations;

namespace AdminWeb.Models
{
    public class AdminLoginModel
    {
        [Required]
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
