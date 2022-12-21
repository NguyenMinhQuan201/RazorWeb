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
        public async Task<JsonResult> GetIndexAsJson()
        {
            var result = await _iAPIHoaDons.GetPagings();
            return Json(new {
                status=true,
                /*data = result.data.OrderBy(x=>x.NgayGio)*/
                data = result.data.Where(x=>x.NgayGio.Value.Year.ToString() == "2022").ToList()
            });
        }
    }
}
