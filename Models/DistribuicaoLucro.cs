namespace ProjetoContabilidade.Models
{
    public class DistribuicaoLucro
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        // Foreign Key para Empresa
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
