using ClinicaPet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaPet.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbClinicaPetContext _context;

        public AccountController(DbClinicaPetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var tutor = await _context.Tutor
                .FirstOrDefaultAsync(t => t.Cpf == model.Cpf);

            if (tutor == null)
            {
                ModelState.AddModelError("", "CPF não encontrado.");
                return View(model);
            }

            // Salva na Session
            HttpContext.Session.SetInt32("TutorId", tutor.Codigo);
            HttpContext.Session.SetString("TutorNome", tutor.Nome);

            return RedirectToAction("Index", "Agendamento");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpa a sessão
            return RedirectToAction("Login");
        }

    }
}
