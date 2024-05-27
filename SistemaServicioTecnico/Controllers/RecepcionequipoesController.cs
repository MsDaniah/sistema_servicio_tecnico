using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaServicioTecnico.Models;

namespace SistemaServicioTecnico.Controllers
{
    public class RecepcionequipoesController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public RecepcionequipoesController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Recepcionequipoes
        public async Task<IActionResult> Index()
        {
            // return View(await _context.Recepcionequipos.ToListAsync());

            var mercyDeveloperContext = _context.Recepcionequipos
                .Include(d => d.Servicio)
                .Include(d => d.Cliente);
            return View(await mercyDeveloperContext.ToListAsync());
        }

        // GET: Recepcionequipoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcionequipo = await _context.Recepcionequipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recepcionequipo == null)
            {
                return NotFound();
            }

            return View(recepcionequipo);
        }

        // GET: Recepcionequipoes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id");
            return View();
        }

        // POST: Recepcionequipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioId,ClienteId,Fecha,TipoPc,Accesorio,MarcaPc,ModeloPc,Nserie,CapacidadRam,TipoAlmacenamiento,CapacidadAlmacenamiento,TipoGpu,Grafico")] Recepcionequipo recepcionequipo)
        {
            if (recepcionequipo.ServicioId != 0 && recepcionequipo.ClienteId != 0 && recepcionequipo.Fecha != null && recepcionequipo.TipoPc != 0 && recepcionequipo.Accesorio != null && recepcionequipo.MarcaPc != null && recepcionequipo.ModeloPc != null && recepcionequipo.Nserie != null && recepcionequipo.CapacidadRam != 0 && recepcionequipo.TipoAlmacenamiento != 0 && recepcionequipo.CapacidadAlmacenamiento != null && recepcionequipo.TipoGpu != 0 && recepcionequipo.Grafico != null)
            {
                _context.Add(recepcionequipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", recepcionequipo.ClienteId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", recepcionequipo.ServicioId);
            return View(recepcionequipo);
        }

        // GET: Recepcionequipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcionequipo = await _context.Recepcionequipos.FindAsync(id);
            if (recepcionequipo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", recepcionequipo.ClienteId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", recepcionequipo.ServicioId);
            return View(recepcionequipo);
        }

        // POST: Recepcionequipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServicioId,ClienteId,Fecha,TipoPc,Accesorio,MarcaPc,ModeloPc,Nserie,CapacidadRam,TipoAlmacenamiento,CapacidadAlmacenamiento,TipoGpu,Grafico")] Recepcionequipo recepcionequipo)
        {
            if (id != recepcionequipo.Id)
            {
                return NotFound();
            }

            if (recepcionequipo.ServicioId != 0 && recepcionequipo.ClienteId != 0 && recepcionequipo.Fecha != null && recepcionequipo.TipoPc != 0 && recepcionequipo.Accesorio != null && recepcionequipo.MarcaPc != null && recepcionequipo.ModeloPc != null && recepcionequipo.Nserie != null && recepcionequipo.CapacidadRam != 0 && recepcionequipo.TipoAlmacenamiento != 0 && recepcionequipo.CapacidadAlmacenamiento != null && recepcionequipo.TipoGpu != 0 && recepcionequipo.Grafico != null)
            {
                try
                {
                    _context.Update(recepcionequipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecepcionequipoExists(recepcionequipo.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", recepcionequipo.ClienteId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", recepcionequipo.ServicioId);
            return View(recepcionequipo);
        }

        // GET: Recepcionequipoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcionequipo = await _context.Recepcionequipos
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recepcionequipo == null)
            {
                return NotFound();
            }

            return View(recepcionequipo);
        }

        // POST: Recepcionequipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recepcionequipo = await _context.Recepcionequipos.FindAsync(id);
            if (recepcionequipo != null)
            {
                _context.Recepcionequipos.Remove(recepcionequipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecepcionequipoExists(int id)
        {
            return _context.Recepcionequipos.Any(e => e.Id == id);
        }
    }
}
