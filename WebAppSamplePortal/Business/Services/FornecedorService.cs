using System.Collections.Generic;
using WebAppSamplePortal.Business.Models;
using System.Linq;
using WebAppSamplePortal.Data.Repositories;
using WebAppSamplePortal.Data.Models;

namespace WebAppSamplePortal.Business.Services
{
    public class FornecedorService
    {
        private readonly FornecedorRepository _fRepo;
        private readonly FornecedorRecursoRepository _rRepo;

        public FornecedorService(FornecedorRepository fRepo, FornecedorRecursoRepository rRepo)
        {
            _fRepo = fRepo;
            _rRepo = rRepo;
        }

        public FornecedorBModel CriarNovoFornecedor(FornecedorBModel fornecedor)
        {
            // PESSOA FISICA = 1
            // PESSOA JURIDICA = 2

            var cdFornecedor = _fRepo.Criar(new FornecedorDbModel
            {
                CodigoEntidade = 10,
                CodigoTipoFornecedor = !string.IsNullOrEmpty(fornecedor.Cnpj) ? 2 : 1,
                RazaoSocial = fornecedor.RazaoSocial,
                NomeFantasia = fornecedor.NomeFantasia,
                CNPJ = fornecedor.Cnpj,
                CPF = fornecedor.Cpf
            });

            var novoFornecedor = _fRepo.ObterFornecedor(cdFornecedor);

            return new FornecedorBModel{
                CodigoEntidade = novoFornecedor.CodigoEntidade,
                CodigoTipoFornecedor = novoFornecedor.CodigoTipoFornecedor,
                Codigo = novoFornecedor.CodigoFornecedor,
                Cnpj = novoFornecedor.CNPJ,
                Cpf = novoFornecedor.CPF,
                RazaoSocial = novoFornecedor.RazaoSocial,
                NomeFantasia = novoFornecedor.NomeFantasia
            };
        }

        public IEnumerable<FornecedorBModel> ObterFornecedores()
        {
            return _fRepo.ObterFornecedores()
                .Select(f => new FornecedorBModel
                {
                    Codigo = f.CodigoFornecedor,
                    CodigoEntidade = f.CodigoEntidade,
                    CodigoTipoFornecedor = f.CodigoTipoFornecedor,
                    Cnpj = f.CNPJ,
                    Cpf = f.CPF,
                    NomeFantasia = f.NomeFantasia,
                    RazaoSocial = f.RazaoSocial
                });
        }

        public IEnumerable<FornecedorRecursoBModel> ObterRecursoDoFornecedor(int codigoFornecedor)
        {
            return _rRepo.ObterRecursosDoFornecedor(codigoFornecedor)
                .Select(r => new FornecedorRecursoBModel
                {
                    Codigo = r.CodigoFornecedorRecurso,
                    DataHoraRecurso = $"{r.DataRecurso} - {r.HoraRecurso}",
                    DataHoraJulgamento = $"{r.DataResposta} - {r.HoraResposta}",
                    Pedido = r.Pedido,
                    Julgamento = r.Julgamento
                });
        }
    }
}
