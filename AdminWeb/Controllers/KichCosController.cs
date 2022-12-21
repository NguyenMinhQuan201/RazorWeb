using AdminWeb.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class KichCosController : Controller
    {
        private readonly IAPISKichCo _iAPISKichCo;
        public KichCosController(IAPISKichCo iAPISKichCo)
        {
            _iAPISKichCo = iAPISKichCo;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _iAPISKichCo.GetKichCoPagings();
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
        public async Task<IActionResult> Create([Bind("ID,KichCoSP")] KichCo request)
        {
            if (ModelState.IsValid)
            {
                var result = await _iAPISKichCo.CreateKichCo(request);
                if (result.IsSuccessed)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(request);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var loaiSanPham = await _iAPISKichCo.GetByIdLoaiKichCo(id.Value);
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
            var loaiSanPham = await _iAPISKichCo.GetByIdLoaiKichCo(id);
            if (loaiSanPham != null)
            {
                await _iAPISKichCo.DeleteKichCo(id);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var loaiSanPham = await _iAPISKichCo.GetByIdLoaiKichCo(id.Value);
            if (loaiSanPham.data == null)
            {
                return NotFound();
            }
            return View(loaiSanPham.data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,KichCoSP")] KichCo rq)
        {
            if (id != rq.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _iAPISKichCo.Update(rq);
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
