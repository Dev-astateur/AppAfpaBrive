using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers.AnnuaireSocialLayer;
using Microsoft.AspNetCore.Routing;

namespace AppAfpaBrive.Web.Controllers.AnnuaireSocial
{
    public class LigneAnnuairesController : Controller
    {
        private readonly AFPANADbContext _context;
        private readonly LigneAnnuaireLayer _ligneAnnuaireLayer;

        public LigneAnnuairesController(AFPANADbContext context)
        {
            _context = context;
            _ligneAnnuaireLayer = new LigneAnnuaireLayer(_context);
        }

        // GET: LigneAnnuaires
        public async Task<IActionResult> Index(string filter, int page, string sortExpression = "PublicConcerne")
        {
            var model = await _ligneAnnuaireLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }

        // GET: LigneAnnuaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneAnnuaire = await _context.LigneAnnuaires
                .Include(l => l.Structure)
                .FirstOrDefaultAsync(m => m.IdLigneAnnuaire == id);
            if (ligneAnnuaire == null)
            {
                return NotFound();
            }

            return View(ligneAnnuaire);
        }

        // GET: LigneAnnuaires/Create
        public IActionResult Create()
        {
            ViewData["IdStructure"] = new SelectList(_context.Structures, "IdStructure", "IdStructure");
            return View();
        }

        // POST: LigneAnnuaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLigneAnnuaire,PublicConcerne,ServiceAbrege,Service,Conditions,IdStructure")] LigneAnnuaire ligneAnnuaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ligneAnnuaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStructure"] = new SelectList(_context.Structures, "IdStructure", "IdStructure", ligneAnnuaire.IdStructure);
            return View(ligneAnnuaire);
        }

        // GET: LigneAnnuaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneAnnuaire = await _context.LigneAnnuaires.FindAsync(id);
            if (ligneAnnuaire == null)
            {
                return NotFound();
            }
            ViewData["IdStructure"] = new SelectList(_context.Structures, "IdStructure", "IdStructure", ligneAnnuaire.IdStructure);
            return View(ligneAnnuaire);
        }

        // POST: LigneAnnuaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLigneAnnuaire,PublicConcerne,ServiceAbrege,Service,Conditions,IdStructure")] LigneAnnuaire ligneAnnuaire)
        {
            if (id != ligneAnnuaire.IdLigneAnnuaire)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ligneAnnuaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LigneAnnuaireExists(ligneAnnuaire.IdLigneAnnuaire))
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
            ViewData["IdStructure"] = new SelectList(_context.Structures, "IdStructure", "IdStructure", ligneAnnuaire.IdStructure);
            return View(ligneAnnuaire);
        }

        // GET: LigneAnnuaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ligneAnnuaire = await _context.LigneAnnuaires
                .Include(l => l.Structure)
                .FirstOrDefaultAsync(m => m.IdLigneAnnuaire == id);
            if (ligneAnnuaire == null)
            {
                return NotFound();
            }

            return View(ligneAnnuaire);
        }

        // POST: LigneAnnuaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ligneAnnuaire = await _context.LigneAnnuaires.FindAsync(id);
            _context.LigneAnnuaires.Remove(ligneAnnuaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LigneAnnuaireExists(int id)
        {
            return _context.LigneAnnuaires.Any(e => e.IdLigneAnnuaire == id);
        }
    }
}
