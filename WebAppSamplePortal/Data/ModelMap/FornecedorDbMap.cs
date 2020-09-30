using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppSamplePortal.Data.Models;

namespace WebAppSamplePortal.Data.ModelMap
{
    public class FornecedorDbMap : IEntityTypeConfiguration<FornecedorDbModel>
    {
        public void Configure(EntityTypeBuilder<FornecedorDbModel> builder)
        {
            builder.ToTable("F233_FORNECEDORES");
            builder.HasKey(t => t.CodigoFornecedor);

            builder.Property(t => t.CodigoEntidade)
                .HasColumnName("CD_ENTIDADE");

            builder.Property(t => t.CodigoTipoFornecedor)
                .HasColumnName("CD_TIPO_FORNECEDOR");

            builder.Property(t => t.CodigoFornecedor)
                .HasColumnName("CD_FORNECEDOR");

            builder.Property(t => t.RazaoSocial)
                .HasColumnName("RAZAO_SOCIAL");

            builder.Property(t => t.NomeFantasia)
                .HasColumnName("NOME_FANTASIA");

            builder.Property(t => t.CNPJ)
                .HasColumnName("CNPJ");

            builder.Property(t => t.CPF)
                .HasColumnName("CPF");

            builder.Property(t => t.Email)
                .HasColumnName("EMAIL");
        }
    }
}
