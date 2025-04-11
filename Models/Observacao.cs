namespace ProjetoContabilidade.Models
{
    public class Observacao
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }

        // Foreign Key para Empresa
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
