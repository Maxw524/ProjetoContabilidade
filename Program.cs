using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoContabilidade.Data;
using ProjetoContabilidade.Models; // Inclui o ApplicationUser, se estiver usando

var builder = WebApplication.CreateBuilder(args);

// Configuração da conexão com o SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura o Identity com ApplicationUser (ou IdentityUser se preferir o padrão)
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Adiciona suporte a controllers, views e Razor Pages (para login/register)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Ativa autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapeia rotas para controllers e páginas Razor (Identity)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Necessário para páginas de Login/Registro

app.Run();
