namespace ProjetoContabilidade.Models
{
    public class Contador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public string IdentityUserId { get; set; }

        // Relação: um contador pode ter várias empresas vinculadas
        public ICollection<Empresa> Empresas { get; set; }
    }
}
