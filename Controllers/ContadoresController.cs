using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoContabilidade.Data;
using ProjetoContabilidade.Models;
using System.Security.Claims;

namespace ProjetoContabilidade.Controllers
{
    [Authorize] // Garante que só usuários autenticados acessem
    public class ContadoresController : Controller
    {
        private readonly ApplicationDbContext _context; // <-- Adicionado aqui
        private readonly UserManager<ApplicationUser> _userManager;

        public ContadoresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Contadores
        public async Task<IActionResult> Index()
        {
            var contadores = await _context.Contadores.ToListAsync();
            return View(contadores);
        }

        // GET: Contadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var contador = await _context.Contadores
                .Include(c => c.Empresas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contador == null)
                return NotFound();

            return View(contador);
        }

        // GET: Contadores/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Contador contador)
        {
            if (ModelState.IsValid)
            {
                _context.Contadores.Add(contador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contador);
        }

        // Método auxiliar para obter o ID do usuário logado
        private string GetLoggedUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
