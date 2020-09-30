namespace WebAppSamplePortal.Data.Models
{
    //[MySqlCharset("utf8")]
    public class FornecedorDbModel
    {
        public int CodigoFornecedor { get; set; }
        public int CodigoTipoFornecedor { get; set; }
        public int CodigoEntidade { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
    }
}
