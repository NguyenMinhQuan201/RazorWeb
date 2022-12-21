using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ChiTietSanPham
    {
        public int Id { get; set; }
        public int IdsanPham { get; set; }
        public string? Ten { get; set; }
        public int? IDLoaiSanPham { get; set; }
        public string? Images { get; set; }
        public decimal? Gia { get; set; }
        public string? Mota { get; set; }
        public string? MauSacSp { get; set; }
        public string? KichCoSp { get; set; }
        public int? SoLuong { get; set; }
        public int? LuotXem { get; set; }
        public decimal? GiaNhap { get; set; }

        public virtual SanPham IdsanPhamNavigation { get; set; } = null!;
    }
}
