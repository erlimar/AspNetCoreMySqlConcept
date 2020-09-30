using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using WebAppSamplePortal.Data.Abstractions;
using WebAppSamplePortal.Data.Models;

namespace WebAppSamplePortal.Data.Repositories
{
    public class FornecedorRepository
    {
        LegacyDbContext _context;
        ILogger<FornecedorRepository> _logger;

        public FornecedorRepository(UnitOfWorkProperty uow, ILogger<FornecedorRepository> logger)
        {
            _context = uow.Property<LegacyDbContext>();
            _logger = logger;
        }

        public FornecedorDbModel ObterFornecedor(int codigo)
        {
            return _context.Set<FornecedorDbModel>().Find(codigo);
        }

        public IEnumerable<FornecedorDbModel> ObterFornecedores()
        {
            return _context.Set<FornecedorDbModel>()
                .OrderByDescending(o => o.CodigoFornecedor)
                .Take(20);
        }

        public int Criar(FornecedorDbModel fornecedor)
        {
            _logger.LogInformation($"Criando fornecedor ${fornecedor.RazaoSocial}");
            _logger.LogInformation($"CD_FORNECEDOR inicial: {fornecedor.CodigoFornecedor}");

            _context.Add<FornecedorDbModel>(fornecedor);
            _context.SaveChanges();

            _logger.LogInformation($"CD_FORNECEDOR atribuído: {fornecedor.CodigoFornecedor}");

            return fornecedor.CodigoFornecedor;
        }
    }
}
