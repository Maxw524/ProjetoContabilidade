namespace ProjetoContabilidade.Models
{
    public class Socio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        // Foreign Key para Empresa
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
