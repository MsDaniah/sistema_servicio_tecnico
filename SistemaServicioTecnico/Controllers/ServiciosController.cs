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
    public class ServiciosController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public ServiciosController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
           // return View(await _context.Servicios.ToListAsync());
            var mercyDeveloperContext = _context.Servicios.Include(e => e.Usuarios);
            return View(await mercyDeveloperContext.ToListAsync());
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Precio,Sku,UsuariosId")] Servicio servicio)
        { //falto los "Validadores " por asi decirlo , es una forma de hacer funcar el projeco , en donde si el nombre es null no te deje avanaar , y el significado de 
            //"!=" es que desigueldad ,comprueba si los operadores son iguales . para mas claridad puedes ver aca su funcion https://learn.microsoft.com/es-es/dotnet/csharp/language-reference/operators/equality-operators
            //de hecho es mucho mejor hacerlo con en foreah es un poco mas largo pero es mucho mas factible y seguro 
            if (servicio.Nombre != null && servicio.Precio != 0 && servicio.Sku != null && servicio.UsuariosId != 0)
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Nombre", servicio.UsuariosId);
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Id", servicio.UsuariosId);
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nombre,Precio,Sku,UsuariosId")] Servicio servicio)
        {
            if (id != servicio.Id)
            {
                return NotFound();
            }

            if (servicio.Id != 0 && servicio.Nombre != null && servicio.Precio != 0 && servicio.Sku != null && servicio.UsuariosId != 0)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.Id))
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
            ViewData["UsuariosId"] = new SelectList(_context.Usuarios, "Id", "Id", servicio.UsuariosId);
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.Id == id);
        }
    }
}