
using AdminWeb.Models;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAPINguoiDung _aPINguoiDung;
        private readonly IConfiguration _configuration;
        public AdminController(IAPINguoiDung aPINguoiDung, IConfiguration configuration)
        {
            _aPINguoiDung = aPINguoiDung;
            _configuration = configuration;
        }
        /*private static int i;*/
        // GET: AdminLogin
        [HttpGet]
        public ActionResult Index()
        {
            var sessions = HttpContext.Session.GetString("Token");
            if (sessions == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        
        public async Task<ActionResult> Index(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await _aPINguoiDung.GetToken(model);
                if (token.token == null)
                {
                    ModelState.AddModelError("", token.message);
                }
                HttpContext.Session.SetString("Token", token.token.accessToken);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "ten dang nhap hoac mat khau ko dung");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Admin");
        }
    }
}