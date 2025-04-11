namespace ProjetoContabilidade.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }

        // Foreign Key para o contador
        public int ContadorId { get; set; }
        public Contador Contador { get; set; }

        public ICollection<Socio> Socios { get; set; }
        public ICollection<DistribuicaoLucro> DistribuicoesLucro { get; set; }
        public ICollection<Pagamento> Pagamentos { get; set; }
        public ICollection<Observacao> Observacoes { get; set; }
    }
}
