using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using AdminWeb.Services;
using RazorWeb.Services;

namespace AdminWeb.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly string _uerContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user_content";
        private readonly IAPISanPham _iAPISanPham;
        private readonly IAPILoaiSanPham _aPILoaiSanPham;
        public SanPhamsController(IWebHostEnvironment webHostEnviroment, IAPISanPham iAPISanPham, IAPILoaiSanPham aPILoaiSanPham)
        {
            _uerContentFolder = Path.Combine(webHostEnviroment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            _iAPISanPham = iAPISanPham;
            _aPILoaiSanPham = aPILoaiSanPham;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            var result = await _iAPISanPham.GetSanPhamPagings();
            return View(result.data);
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _iAPISanPham.GetByIdSanPham(id.Value);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham.data);
        }

        // GET: SanPhams/Create
        public async Task<IActionResult> Create()
        {
            var selectListItems = await _aPILoaiSanPham.GetLoaiSanPhamPagings();
            ViewData["IdloaiSanPham"] = new SelectList(selectListItems.data, "IDLoaiSanPham", "Ten");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDSanPham,Ten,IDLoaiSanPham,Images,Gia,Mota,Rating,GiaNhap")] SanPham sanPham)
        {
            var f = Request.Form.Files["image"];
            if(f!= null)
            {
                try
                {
                    var filePath = Path.Combine(_uerContentFolder, f.FileName);
                    using var output = new FileStream(filePath, FileMode.Create);
                    await f.OpenReadStream().CopyToAsync(output);
                    sanPham.Images = f.FileName.ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine("lỗi");
                }
            }
            if (ModelState.IsValid)
            {
                await _iAPISanPham.CreateSanPham(sanPham);
                return RedirectToAction(nameof(Index));
            }
            var selectItem = await _aPILoaiSanPham.GetLoaiSanPhamPagings();
            ViewData["IdloaiSanPham"] = new SelectList(selectItem.data, "IDLoaiSanPham", "Ten", sanPham.IDLoaiSanPham);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _iAPISanPham.GetByIdSanPham(id.Value);
            if (sanPham == null)
            {
                return NotFound();
            }
            var selectItem = await _aPILoaiSanPham.GetLoaiSanPhamPagings();
            ViewData["IdloaiSanPham"] = new SelectList(selectItem.data, "IDLoaiSanPham", "Ten", sanPham.data.IDLoaiSanPham);
            return View(sanPham.data);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDSanPham,Ten,IDLoaiSanPham,Images,Gia,Mota,Rating,GiaNhap")] SanPham sanPham)
        {
            if (id != sanPham.IDLoaiSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _iAPISanPham.UpdateSanPham(sanPham);
                }
                catch (Exception e)
                {
                    Console.WriteLine("loi",e);
                }
                return RedirectToAction(nameof(Index));
            }
            var selectItem = await _aPILoaiSanPham.GetLoaiSanPhamPagings();
            ViewData["IdloaiSanPham"] = new SelectList(selectItem.data, "IDLoaiSanPham", "Ten", sanPham.IDLoaiSanPham);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _iAPISanPham.GetByIdSanPham(id.Value);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham.data);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _iAPISanPham.GetByIdSanPham(id);
            if (sanPham != null)
            {
                await _iAPISanPham.DeleteSanPham(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
