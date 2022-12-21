using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class NguoiDung
    {
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? Email { get; set; }
        public int? Phone { get; set; }
        public int Id { get; set; }
        public string? DiaChi { get; set; }
        public string? Image { get; set; }
    }
}
