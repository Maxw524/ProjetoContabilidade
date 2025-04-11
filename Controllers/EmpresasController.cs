using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoContabilidade.Data;
using ProjetoContabilidade.Models;
using System.Security.Claims;

namespace ProjetoContabilidade.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmpresasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Empresas
        public async Task<IActionResult> Index(string pesquisa)
        {
            var empresas = _context.Empresas
                .Include(e => e.Contador)
                .AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                empresas = empresas.Where(e => e.RazaoSocial.Contains(pesquisa) ||
                                               e.CNPJ.Contains(pesquisa));
            }

            return View(await empresas.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var empresa = await _context.Empresas
                .Include(e => e.Contador)
                .Include(e => e.Socios)
                .Include(e => e.DistribuicoesLucro)
                .Include(e => e.Pagamentos)
                .Include(e => e.Observacoes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (empresa == null)
                return NotFound();

            ViewBag.PodeEditar = UsuarioPodeEditar(empresa);

            return View(empresa);
        }

        // Método auxiliar para verificar se o usuário logado pode editar essa empresa
        private bool UsuarioPodeEditar(Empresa empresa)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return UsuarioEhAdministrador() || (empresa.Contador != null && empresa.Contador.IdentityUserId == userId);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
                return NotFound();

            if (!UsuarioPodeEditar(empresa))
                return Forbid(); // Retorna erro 403 - Acesso proibido

            return View(empresa);
        }

        // POST: Empresas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empresa empresa)
        {
            if (id != empresa.Id)
                return NotFound();

            var empresaOriginal = await _context.Empresas
                .Include(e => e.Contador)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (empresaOriginal == null)
                return NotFound();

            if (!UsuarioPodeEditar(empresaOriginal))
                return Forbid(); // impede alteração se não for o contador vinculado ou administrador

            if (ModelState.IsValid)
            {
                // Atualiza todos os campos
                empresaOriginal.RazaoSocial = empresa.RazaoSocial;
                empresaOriginal.CNPJ = empresa.CNPJ;
                empresaOriginal.Endereco = empresa.Endereco;
                // Atualize outros campos conforme necessário

                _context.Update(empresaOriginal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = empresa.Id });
            }
            return View(empresa);
        }

        private bool UsuarioEhAdministrador()
        {
            return User.IsInRole("Administrador");
        }
    }
}
