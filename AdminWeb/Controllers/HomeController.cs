using AdminWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPIHoaDons _iAPIHoaDons;
        public HomeController(IAPIHoaDons iAPIHoaDons)
        {
            _iAPIHoaDons = iAPIHoaDons;
        }
        public async Task<IActionResult> Index()
        {
            var years = await _iAPIHoaDons.GetYears();
            int i=0;
            foreach (var year in years.data)
            {
                year.Id = i;
                i++;
            }
            ViewBag.Years = new SelectList(years.data, "Id", "Year");
            return View();
        }
    }
}
