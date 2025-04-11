namespace ProjetoContabilidade.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }

        // Foreign Key para Empresa
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
