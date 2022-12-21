using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using AdminWeb.Services;
using AdminWeb.Models.VM;

namespace AdminWeb.Controllers
{
    public class ChiTietSanPhamsController : Controller
    {
        private readonly IAPIChiTietSanPham _chiTietSanPham;
        private readonly IAPISanPham _aPISanPham;
        private readonly string _uerContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user_content";
        public ChiTietSanPhamsController(IAPIChiTietSanPham chiTietSanPham, IWebHostEnvironment webHostEnviroment, IAPISanPham aPISanPham)
        {
            _uerContentFolder = Path.Combine(webHostEnviroment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            _chiTietSanPham = chiTietSanPham;
            _aPISanPham = aPISanPham;
        }

        // GET: ChiTietSanPhams
        public async Task<IActionResult> Index()
        {
            var result = await _chiTietSanPham.GetChiTietSanPhamPagings();
            return View(result.data);
        }

        // GET: ChiTietSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var chiTietSanPham = await _chiTietSanPham.GetByIdChiTietSanPham(id.Value);
            if (chiTietSanPham.data == null)
            {
                return NotFound();
            }

            return View(chiTietSanPham.data);
        }

        // GET: ChiTietSanPhams/Create
        public async Task<IActionResult> Create()
        {
            var getsanpham = await _aPISanPham.GetSanPhamPagings();
            ViewData["IdsanPham"] = new SelectList(getsanpham.data, "IdsanPham", "IdsanPham");
            return View();
        }

        // POST: ChiTietSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IDSanPham,Ten,Images,Gia,Mota,MauSacSP,KichCoSP,SoLuong,LuotXem,GiaNhap")] ChiTietSanPhamVM chiTietSanPham)
        {
            var f = Request.Form.Files["image"];
            if (f != null)
            {
                try
                {
                    var filePath = Path.Combine(_uerContentFolder, f.FileName);
                    using var output = new FileStream(filePath, FileMode.Create);
                    await f.OpenReadStream().CopyToAsync(output);
                    chiTietSanPham.Images = f.FileName.ToString();
                    if (ModelState.IsValid)
                    {
                        var rq = await _chiTietSanPham.CreateChiTietSanPham(chiTietSanPham);
                        if (rq.IsSuccessed)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("lỗi");
                }
            }
            var getsanpham = await _aPISanPham.GetSanPhamPagings();
            ViewData["IdsanPham"] = new SelectList(getsanpham.data, "IdsanPham", "IdsanPham", chiTietSanPham.IDSanPham);
            return View(chiTietSanPham);
        }

        // GET: ChiTietSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var chiTietSanPham = await _chiTietSanPham.GetByIdChiTietSanPham(id.Value);
            if (chiTietSanPham == null)
            {
                return NotFound();
            }//API cua SanPham
            var getsanpham = await _aPISanPham.GetSanPhamPagings();
            ViewData["IdsanPham"] = new SelectList(getsanpham.data, "IdsanPham", "IdsanPham", chiTietSanPham.data.ID);
            return View(chiTietSanPham.data);
        }

        // POST: ChiTietSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IDSanPham,Ten,Images,Gia,Mota,MauSacSP,KichCoSP,SoLuong,LuotXem,GiaNhap")] ChiTietSanPhamVM chiTietSanPham)
        {
            if (id != chiTietSanPham.ID)
            {
                return NotFound();
            }

            var f = Request.Form.Files["image"];
            if (f != null)
            {
                try
                {
                    var filePath = Path.Combine(_uerContentFolder, f.FileName);
                    using var output = new FileStream(filePath, FileMode.Create);
                    await f.OpenReadStream().CopyToAsync(output);
                    chiTietSanPham.Images = f.FileName.ToString();
                    if (ModelState.IsValid)
                    {
                        var rq = await _chiTietSanPham.UpdateChiTietSanPham(chiTietSanPham);
                        if (rq.IsSuccessed)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("lỗi");
                }
            }
            var getsanpham = await _aPISanPham.GetSanPhamPagings();
            ViewData["IdsanPham"] = new SelectList(getsanpham.data, "IdsanPham", "IdsanPham", chiTietSanPham.ID);
            return View(chiTietSanPham);
        }

        // GET: ChiTietSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            

            var chiTietSanPham = await _chiTietSanPham.GetByIdChiTietSanPham(id.Value);
            if (chiTietSanPham == null)
            {
                return NotFound();
            }

            return View(chiTietSanPham.data);
        }

        // POST: ChiTietSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietSanPham = await _chiTietSanPham.GetByIdChiTietSanPham(id);
            if (chiTietSanPham != null)
            {
                await _chiTietSanPham.DeleteChiTietSanPham(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
