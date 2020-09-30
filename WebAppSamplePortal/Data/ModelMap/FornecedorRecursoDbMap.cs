using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppSamplePortal.Data.Models;

namespace WebAppSamplePortal.Data.ModelMap
{
    public class FornecedorRecursoDbMap : IEntityTypeConfiguration<FornecedorRecursoDbModel>
    {
        public void Configure(EntityTypeBuilder<FornecedorRecursoDbModel> builder)
        {
            builder.ToTable("F233_FORNECEDORES_RECURSOS");
            builder.HasKey(t => t.CodigoFornecedorRecurso);

            builder.Property(t => t.CodigoFornecedorRecurso)
                .HasColumnName("CD_FORNECEDOR_RECURSO");

            builder.Property(t => t.CodigoFornecedor)
                .HasColumnName("CD_FORNECEDOR");

            builder.Property(t => t.CodigoLicitacao)
                .HasColumnName("CD_LICITACAO");

            builder.Property(t => t.DataResposta)
                .HasColumnName("DATA_RESPOSTA");

            builder.Property(t => t.HoraResposta)
                .HasColumnName("HORA_RESPOSTA");

            builder.Property(t => t.DataRecurso)
                .HasColumnName("DATA_RECURSO");

            builder.Property(t => t.HoraRecurso)
                .HasColumnName("HORA_RECURSO");

            builder.Property(t => t.Pedido)
                .HasColumnName("PEDIDO");

            builder.Property(t => t.Julgamento)
                .HasColumnName("JULGAMENTO");
        }
    }
}
