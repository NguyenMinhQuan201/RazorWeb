using AdminWeb.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class MauSacsController : Controller
    {
        private readonly IAPIMauSac _aPIMauSac;
        public MauSacsController(IAPIMauSac aPIMauSac)
        {
            _aPIMauSac = aPIMauSac;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _aPIMauSac.GetPagings();
            if (result == null)
            {
                RedirectToAction("Index", "Admin");
            }
            return View(result.data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MauSacSP")] MauSac request)
        {
            if (ModelState.IsValid)
            {
                var result = await _aPIMauSac.Create(request);
                if (result.IsSuccessed)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(request);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiSanPham = await _aPIMauSac.GetById(id.Value);
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
            var loaiSanPham = await _aPIMauSac.GetById(id);
            if (loaiSanPham != null)
            {
                await _aPIMauSac.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var loaiSanPham = await _aPIMauSac.GetById(id.Value);
            if (loaiSanPham.data == null)
            {
                return NotFound();
            }
            return View(loaiSanPham.data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MauSacSP")] MauSac rq)
        {
            if (id != rq.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _aPIMauSac.Update(rq);
                }
                catch (Exception e)
                {
                    Console.Write("Loi~", e);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rq);
        }
    }
}
