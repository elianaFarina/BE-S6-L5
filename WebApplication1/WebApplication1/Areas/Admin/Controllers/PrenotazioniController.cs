using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Areas.Amministrazione.Controllers
{
    [Area("Amministrazione")]
    public class PrenotazioneController : Controller
    {
        private readonly HotelDbContext _context;

        public PrenotazioneController(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Prenotazioni.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrenotazioneId,ClienteId,CameraId,DataInizio,DataFine")] Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prenotazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prenotazione);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione == null) return NotFound();
            return View(prenotazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrenotazioneId,ClienteId,CameraId,DataInizio,DataFine")] Prenotazione prenotazione)
        {
            if (id != prenotazione.PrenotazioneId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prenotazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioneExists(prenotazione.PrenotazioneId))
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
            return View(prenotazione);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione == null) return NotFound();
            return View(prenotazione);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione != null)
            {
                _context.Prenotazioni.Remove(prenotazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        private bool PrenotazioneExists(int id)
        {
            return _context.Prenotazioni.Any(e => e.PrenotazioneId == id);
        }
    }
}
