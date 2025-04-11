
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoContabilidade.Models;

namespace ProjetoContabilidade.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contador> Contadores { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<DistribuicaoLucro> DistribuicoesLucro { get; set; }
        public DbSet<Observacao> Observacoes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Regras e relacionamentos adicionais, se necessário
            modelBuilder.Entity<Contador>()
                .HasMany(c => c.Empresas)
                .WithOne(e => e.Contador)
                .HasForeignKey(e => e.ContadorId);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Socios)
                .WithOne(s => s.Empresa)
                .HasForeignKey(s => s.EmpresaId);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Observacoes)
                .WithOne(o => o.Empresa)
                .HasForeignKey(o => o.EmpresaId);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.DistribuicoesLucro)
                .WithOne(d => d.Empresa)
                .HasForeignKey(d => d.EmpresaId);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Pagamentos)
                .WithOne(p => p.Empresa)
                .HasForeignKey(p => p.EmpresaId);
        }
    }
}
