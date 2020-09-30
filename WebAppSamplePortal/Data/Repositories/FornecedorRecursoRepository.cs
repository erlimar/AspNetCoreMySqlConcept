using System.Collections.Generic;
using System.Linq;
using WebAppSamplePortal.Data.Abstractions;
using WebAppSamplePortal.Data.Models;

namespace WebAppSamplePortal.Data.Repositories
{
    public class FornecedorRecursoRepository
    {
        LegacyDbContext _context;

        public FornecedorRecursoRepository(UnitOfWorkProperty<LegacyDbContext> context)
        {
            _context = context;
        }

        public IEnumerable<FornecedorRecursoDbModel> ObterRecursosDoFornecedor(int codigoFornecedor)
        {
            return _context.Set<FornecedorRecursoDbModel>()
                .Where(w => w.CodigoFornecedor == codigoFornecedor)
                .Take(100);
        }
    }
}
