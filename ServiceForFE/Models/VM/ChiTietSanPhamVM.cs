namespace AdminWeb.Models.VM
{
    public class ChiTietSanPhamVM
    {
        public int ID { get; set; }
        public int IDSanPham { get; set; }
        public string? Ten { get; set; }
        public int? IDLoaiSanPham { get; set; }
        public string? Images { get; set; }
        public decimal? Gia { get; set; }
        public string? Mota { get; set; }
        public string? MauSacSP { get; set; }
        public string? KichCoSP { get; set; }
        public int? SoLuong { get; set; }
        public int? LuotXem { get; set; }
        public decimal? GiaNhap { get; set; }
    }
}
