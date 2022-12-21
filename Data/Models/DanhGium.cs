using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class DanhGium
    {
        public string? Id { get; set; }
        public int? IdsanPham { get; set; }
        public int? Rating { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? Date { get; set; }
    }
}
