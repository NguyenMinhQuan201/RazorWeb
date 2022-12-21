using AdminWeb.Services;
using Data.Models;

namespace RazorWeb.Models
{
    public interface IEventsModels
    {
        public Task<List<MauSac>> ListAllColor(int id);
        public Task<List<KichCo>> ListAllSize(int id);
    }
    public class EventsModels : IEventsModels
    {
        private readonly IAPISanPham _aPISanPham;
        private readonly IAPITinTuc _aPITinTuc;
        private readonly IAPIChiTietSanPham _apiChiTietSanPham;
        private readonly IAPIMauSac _aPIMauSac;
        private readonly IAPISKichCo _aPISKichCo ;
        public EventsModels(IAPISanPham aPISanPham, IAPITinTuc aPITinTuc, IAPIChiTietSanPham apiChiTietSanPham,IAPISKichCo aPISKichCo,IAPIMauSac aPIMauSac)
        {
            _aPISanPham = aPISanPham;
            _aPITinTuc = aPITinTuc;
            _apiChiTietSanPham = apiChiTietSanPham;
            _aPIMauSac = aPIMauSac;
            _aPISKichCo = aPISKichCo;
        }
        public async Task<List<MauSac>> ListAllColor(int id)
        {
            List<MauSac> a = new List<MauSac>();
            var find = /*_db.ChiTietSanPhams.Where(x => x.IdsanPham == id).ToList();*/ await _apiChiTietSanPham.GetByIdSanPham(id);
            var result = await _aPIMauSac.GetPagings();
            foreach (var item in find.data)
            {
                foreach (var item2 in result.data)
                {
                    if (a.Contains(item2))
                    {
                        int i = 0;
                    }
                    else
                    {
                        if (item.MauSacSp == item2.MauSacSP)
                        {
                            a.Add(item2);
                        }
                    }
                }
            }

            return a;
        }
        public async Task<List<KichCo>> ListAllSize(int id)
        {
            List<KichCo> a = new List<KichCo>();
            var find = await _apiChiTietSanPham.GetByIdSanPham(id);
            var result = await _aPISKichCo.GetKichCoPagings();
            foreach (var item in find.data)
            {
                foreach (var item2 in result.data)
                {
                    if (a.Contains(item2))
                    {
                        int i = 0;
                    }
                    else
                    {
                        if (item.KichCoSp == item2.KichCoSP)
                        {
                            a.Add(item2);
                        }
                    }
                }
            }

            return a;
        }
    }
}
