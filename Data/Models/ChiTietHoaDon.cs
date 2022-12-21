using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ChiTietHoaDon
    {
        public string? Images { get; set; }
        public decimal? Gia { get; set; }
        public string? MauSacSP { get; set; }
        public string? KichCoSP { get; set; }
        public int IDHoaDon { get; set; }
        public int? Soluong { get; set; }

        public virtual HoaDon IdhoaDonNavigation { get; set; } = null!;
    }
}
