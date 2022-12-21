using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class HoaDon
    {

        public int IDHoaDon { get; set; }
        public string? DiaChi { get; set; }
        public int? SDT { get; set; }
        public decimal? Gia { get; set; }
        public string? UserName { get; set; }
        public DateTime? NgayGio { get; set; }
        public decimal? TongGiaNhap { get; set; }
    }
}
