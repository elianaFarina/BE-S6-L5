using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Models;

namespace WebApplication1.Areas.Amministrazione.Controllers
{
    public class CameraController : Controller
    {
        private readonly HotelDbContext _context;

        public CameraController(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Camere.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CameraId,Numero,Descrizione,Prezzo")] Camera camera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(camera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(camera);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var camera = await _context.Camere.FindAsync(id);
            if (camera == null) return NotFound();
            return View(camera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CameraId,Numero,Descrizione,Prezzo")] Camera camera)
        {
            if (id != camera.CameraId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CameraExists(camera.CameraId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(camera);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var camera = await _context.Camere.FindAsync(id);
            if (camera == null) return NotFound();
            return View(camera);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camera = await _context.Camere.FindAsync(id);
            if (camera != null)
            {
                _context.Camere.Remove(camera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        private bool CameraExists(int id)
        {
            return _context.Camere.Any(e => e.CameraId == id);
        }
    }
}
