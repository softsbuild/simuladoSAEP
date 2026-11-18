using Microsoft.AspNetCore.Mvc;
using AppCursosUniversitarios.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace AppCursosUniversitarios.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbCursosEnemContext _context;

        public LoginController(DbCursosEnemContext context)
        {
            _context = context;
        }

        // GET: /Login/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login/
        [HttpPost]
        public IActionResult Index(string email, string senha)
        {
            // Verifica se existe o usuário no banco com o email e senha informados
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);

            if (usuario != null)
            {
                // Se válido: Salva uma flag na Sessão indicando que está logado
                HttpContext.Session.SetString("UsuarioLogado", usuario.Nome);
                return RedirectToAction("Index", "Home");
            }

            // Se inválido: Retorna a view com a mensagem de erro
            ViewBag.Erro = "Usuário ou senha inválidos.";
            return View();
        }

        // Ação para sair do sistema
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}