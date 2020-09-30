namespace WebAppSamplePortal.Data.Models
{
    public class FornecedorRecursoDbModel
    {
        public int CodigoFornecedorRecurso { get; set; }
        public string DataRecurso { get; set; }
        public string HoraRecurso { get; set; }
        public string DataResposta { get; set; }
        public string HoraResposta { get; set; }
        public string Pedido { get; set; }
        public string Julgamento { get; set; }

        public int CodigoFornecedor { get; set; }
        //public FornecedorDbModel Fornecedor { get; set; }
        
        public int CodigoLicitacao { get; set; }
        //public LicitacaoDbModel Licitacao { get; set; }
    }
}
