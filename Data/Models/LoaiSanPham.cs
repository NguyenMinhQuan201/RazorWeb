using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class LoaiSanPham
    {
        public LoaiSanPham()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int IdloaiSanPham { get; set; }
        public string? Ten { get; set; }
        public string? Alias { get; set; }
        public bool? TrangThai { get; set; }
        public int ID { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
