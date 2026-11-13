using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaPet.Models;

namespace ClinicaPet.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly DbClinicaPetContext _context;

        public AgendamentoController(DbClinicaPetContext context)
        {
            _context = context;
        }

        // GET: Agendamento
       
        public async Task<IActionResult> Index()
        {
            // 1. Verifica se a sessão do Tutor existe (se está logado)
            var tutorId = HttpContext.Session.GetInt32("TutorId");

            if (tutorId == null)
            {
                // Se não estiver logado, redireciona para a página de Login
                return RedirectToAction("Login", "Account");
            }

            // 2. Se estiver logado, busca os agendamentos apenas desse Tutor
            var agendamentos = await _context.Agendamentos
                .Include(a => a.CodigoPetsNavigation) // Mantém o include que o sistema gerou
                                                      // .Where(a => a.TutorId == tutorId)  // <- DESCOMENTA esta linha se o teu Agendamento tiver o TutorId direto
                .ToListAsync();

            return View(agendamentos);
        }

        // GET: Agendamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.CodigoPetsNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // GET: Agendamento/Create
        public IActionResult Create()
        {
            ViewData["CodigoPets"] = new SelectList(_context.Pets, "Codigo", "Codigo");
            return View();
        }

        // POST: Agendamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,MotivoConsulta,DateHora,CodigoPets")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoPets"] = new SelectList(_context.Pets, "Codigo", "Codigo", agendamento.CodigoPets);
            return View(agendamento);
        }

        // GET: Agendamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["CodigoPets"] = new SelectList(_context.Pets, "Codigo", "Codigo", agendamento.CodigoPets);
            return View(agendamento);
        }

        // POST: Agendamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,MotivoConsulta,DateHora,CodigoPets")] Agendamento agendamento)
        {
            if (id != agendamento.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.Codigo))
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
            ViewData["CodigoPets"] = new SelectList(_context.Pets, "Codigo", "Codigo", agendamento.CodigoPets);
            return View(agendamento);
        }

        // GET: Agendamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.CodigoPetsNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
            return _context.Agendamentos.Any(e => e.Codigo == id);
        }
    }
}
