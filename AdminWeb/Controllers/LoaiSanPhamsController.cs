using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using RazorWeb.Services;

namespace AdminWeb.Controllers
{
    public class LoaiSanPhamsController : Controller
    {
        private readonly IAPILoaiSanPham _loaiSanPham;

        public LoaiSanPhamsController(IAPILoaiSanPham loaiSanPham)
        {
            _loaiSanPham = loaiSanPham; 
        }
        public async Task<IActionResult> Index()
        {
            var result = await _loaiSanPham.GetLoaiSanPhamPagings();
            if (result == null)
            {
                RedirectToAction("Index","Admin");
            }
            return View(result.data);
        }
        public async Task<IActionResult> Details(int? id)
        {
            var loaiSanPham = await _loaiSanPham.GetByIdLoaiSanPham(id.Value);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham.data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdloaiSanPham,Ten,Alias,TrangThai")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                var result = await _loaiSanPham.CreateLoaiSanPham(loaiSanPham);
                if (result.IsSuccessed)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(loaiSanPham);
        }
        // GET: LoaiSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var loaiSanPham = await _loaiSanPham.GetByIdLoaiSanPham(id.Value);
            if (loaiSanPham.data == null)
            {
                return NotFound();
            }
            return View(loaiSanPham.data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdloaiSanPham,Ten,Alias,TrangThai")] LoaiSanPham loaiSanPham)
        {
            if (id != loaiSanPham.IdloaiSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _loaiSanPham.UpdateLoaiSanPham(loaiSanPham);
                }
                catch (Exception e)
                {
                    Console.Write("Loi~",e);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSanPham);
        }

        // GET: LoaiSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiSanPham = await _loaiSanPham.GetByIdLoaiSanPham(id.Value);
            if (loaiSanPham.data == null)
            {
                return NotFound();
            }

            return View(loaiSanPham.data);
        }

        // POST: LoaiSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSanPham = await _loaiSanPham.GetByIdLoaiSanPham(id);
            if (loaiSanPham != null)
            {
                await _loaiSanPham.DeleteLoaiSanPham(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
