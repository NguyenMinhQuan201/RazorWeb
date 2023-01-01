using AdminWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class HoaDonsController : Controller
    {
        private readonly IAPIHoaDons _iAPIHoaDons;
        public HoaDonsController(IAPIHoaDons iAPIHoaDons)
        {
            _iAPIHoaDons = iAPIHoaDons;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _iAPIHoaDons.GetPagings();
            return View(result.data);
        }
        public async Task<JsonResult> GetIndexAsJson(int? year,int? month)
        {
            var result = await _iAPIHoaDons.GetPagings();
            string year_now = DateTime.Now.Year.ToString(); ;            
            if (year != null)
            {
                year_now = year.ToString();
            }
            if (month != null)
            {
                return Json(new
                {
                    status = true,
                    data = result.data.Where(x => x.NgayGio.Value.Year.ToString() == year_now && x.NgayGio.Value.Month.ToString()== month.ToString()).ToList()
                });
            }
            return Json(new {
                status=true,
                data = result.data.Where(x=>x.NgayGio.Value.Year.ToString() == year_now).ToList()
            });
        }
    }
}
