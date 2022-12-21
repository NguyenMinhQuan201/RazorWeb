using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class CombineConditionChiTietSanPham
    {
        public int IDSanPham { get; set; }
        public string KichCoSP { get; set; }
        public string MauSacSP { get; set; }
    }
    public class MauSacByKichCo
    {
        public string KichCoSP { get; set; }
    }
    public class MauSacByName
    {
        public string MauSacSP { get; set; }
    }
}
