using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class TinTuc
    {
        public int ID { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? TieuDe { get; set; }
        public string? NoiDung { get; set; }
    }
}
