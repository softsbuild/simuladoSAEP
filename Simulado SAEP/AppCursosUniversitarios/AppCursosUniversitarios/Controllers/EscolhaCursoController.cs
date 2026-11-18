using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCursosUniversitarios.Models;

namespace AppCursosUniversitarios.Controllers
{
    public class EscolhaCursoController : Controller
    {
        private readonly DbCursosEnemContext _context;

        public EscolhaCursoController(DbCursosEnemContext context)
        {
            _context = context;
        }

        // GET: EscolhaCurso
        public async Task<IActionResult> Index()
        {
            var dbCursosEnemContext = _context.EscolhaCursos.Include(e => e.CodigoAlunoNavigation).Include(e => e.CodigoCursoNavigation);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioLogado")))
            {
                return RedirectToAction("Index", "Login"); // Manda de volta pro Login
            }

            return View(await _context.Alunos.ToListAsync());
        }

        // GET: EscolhaCurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolhaCurso = await _context.EscolhaCursos
                .Include(e => e.CodigoAlunoNavigation)
                .Include(e => e.CodigoCursoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (escolhaCurso == null)
            {
                return NotFound();
            }

            return View(escolhaCurso);
        }

        // GET: EscolhaCurso/Create
        public IActionResult Create()
        {
            ViewData["CodigoAluno"] = new SelectList(_context.Alunos, "Codigo", "Codigo");
            ViewData["CodigoCurso"] = new SelectList(_context.Cursos, "Codigo", "Codigo");
            return View();
        }

        // POST: EscolhaCurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,CodigoAluno,CodigoCurso,OrdemPreferencial,DataEscolha")] EscolhaCurso escolhaCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escolhaCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAluno"] = new SelectList(_context.Alunos, "Codigo", "Codigo", escolhaCurso.CodigoAluno);
            ViewData["CodigoCurso"] = new SelectList(_context.Cursos, "Codigo", "Codigo", escolhaCurso.CodigoCurso);
            return View(escolhaCurso);
        }

        // GET: EscolhaCurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolhaCurso = await _context.EscolhaCursos.FindAsync(id);
            if (escolhaCurso == null)
            {
                return NotFound();
            }
            ViewData["CodigoAluno"] = new SelectList(_context.Alunos, "Codigo", "Codigo", escolhaCurso.CodigoAluno);
            ViewData["CodigoCurso"] = new SelectList(_context.Cursos, "Codigo", "Codigo", escolhaCurso.CodigoCurso);
            return View(escolhaCurso);
        }

        // POST: EscolhaCurso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,CodigoAluno,CodigoCurso,OrdemPreferencial,DataEscolha")] EscolhaCurso escolhaCurso)
        {
            if (id != escolhaCurso.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escolhaCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscolhaCursoExists(escolhaCurso.Codigo))
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
            ViewData["CodigoAluno"] = new SelectList(_context.Alunos, "Codigo", "Codigo", escolhaCurso.CodigoAluno);
            ViewData["CodigoCurso"] = new SelectList(_context.Cursos, "Codigo", "Codigo", escolhaCurso.CodigoCurso);
            return View(escolhaCurso);
        }

        // GET: EscolhaCurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escolhaCurso = await _context.EscolhaCursos
                .Include(e => e.CodigoAlunoNavigation)
                .Include(e => e.CodigoCursoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (escolhaCurso == null)
            {
                return NotFound();
            }

            return View(escolhaCurso);
        }

        // POST: EscolhaCurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escolhaCurso = await _context.EscolhaCursos.FindAsync(id);
            if (escolhaCurso != null)
            {
                _context.EscolhaCursos.Remove(escolhaCurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscolhaCursoExists(int id)
        {
            return _context.EscolhaCursos.Any(e => e.Codigo == id);
        }
    }
}
