using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietSanPhams = new HashSet<ChiTietSanPham>();
        }

        public int IDSanPham { get; set; }
        public string? Ten { get; set; }
        public int? IDLoaiSanPham { get; set; }
        public string? Images { get; set; }
        public decimal? Gia { get; set; }
        public string? Mota { get; set; }
        public int? Rating { get; set; }
        public decimal? GiaNhap { get; set; }

        public virtual LoaiSanPham? IdloaiSanPhamNavigation { get; set; }
        public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; }
    }
}
