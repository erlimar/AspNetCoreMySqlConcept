using System.ComponentModel.DataAnnotations;

namespace WebAppSamplePortal.ViewModels
{
    public class NovoFornecedorViewModel
    {
        public int CodigoTipoFornecedor { get; set; }
        public int CodigoEntidade { get; set; }

        [Display(Name = "Razão Social")]
        [Required]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome Fantasia")]
        [Required]
        public string NomeFantasia { get; set; }

        [Display(Name = "CNPJ")]
        [RequiredCnpjOrCpf]
        public string Cnpj { get; set; }

        [Display(Name = "CPF")]
        [RequiredCnpjOrCpf]
        public string Cpf { get; set; }
    }

    public class RequiredCnpjOrCpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (NovoFornecedorViewModel)validationContext.ObjectInstance;

            if (string.IsNullOrWhiteSpace(model.Cpf) && string.IsNullOrWhiteSpace(model.Cnpj))
            {
                return new ValidationResult("Você precisa informar CNPJ ou CPF");
            }

            if (!string.IsNullOrWhiteSpace(model.Cpf) && !string.IsNullOrWhiteSpace(model.Cnpj))
            {
                return new ValidationResult("Você precisa informar CNPJ ou CPF. Nunca os dois simultaneamente");
            }

            return ValidationResult.Success;
        }
    }
}