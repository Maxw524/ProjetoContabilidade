using Microsoft.AspNetCore.Identity;

namespace ProjetoContabilidade.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        public int? ContadorId { get; set; }
        public Contador Contador { get; set; }
    }
}
