using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppSamplePortal.Business.Services;
using System.Linq;
using WebAppSamplePortal.Business.Models;
using WebAppSamplePortal.ViewModels;
using System.Threading.Tasks;

namespace WebAppSamplePortal.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly ILogger<FornecedorController> _logger;
        private readonly FornecedorService _svcFornecedor;

        public FornecedorController(ILogger<FornecedorController> logger, FornecedorService fornecedorService)
        {
            _logger = logger;
            _svcFornecedor = fornecedorService;
        }

        public ActionResult Index()
        {
            _logger.LogDebug("Listando fornecedores para tela inicial");
            var fornecedores = _svcFornecedor.ObterFornecedores();

            if (fornecedores == null)
                fornecedores = Enumerable.Empty<FornecedorBModel>();

            if (fornecedores.Count() < 1)
                _logger.LogDebug("Nenhum fornecedor encontrado para listar");

            return View(fornecedores.Select(f => new FornecedorSimplificadoViewModel
            {
                Codigo = f.Codigo,
                Nome = f.RazaoSocial
            }));
        }

        public async Task<IActionResult> Novo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Novo(NovoFornecedorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _svcFornecedor.CriarNovoFornecedor(new FornecedorBModel{
                    RazaoSocial = model.RazaoSocial,
                    NomeFantasia = model.NomeFantasia,
                    Cpf = model.Cpf,
                    Cnpj = model.Cnpj
                });

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public ActionResult Recursos(int id)
        {
            _logger.LogDebug($"Listando recursos para fornecedor {id}");
            var recursos = _svcFornecedor.ObterRecursoDoFornecedor(id);

            if (recursos == null)
                recursos = Enumerable.Empty<FornecedorRecursoBModel>();

            if (recursos.Count() < 1)
                _logger.LogDebug($"Nenhum recurso encontrado para o fornecedor {id}");

            return View(recursos.Select(r => new FornecedorRecursoSimplificadoViewModel
            {
                DataHoraJulgamento = r.DataHoraJulgamento,
                DataHoraRecurso = r.DataHoraRecurso,
                Pedido = r.Pedido,
                Recurso = r.Recurso,
                Julgamento = r.Julgamento
            }));
        }
    }
}
