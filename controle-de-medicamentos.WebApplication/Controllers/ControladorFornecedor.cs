using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeMedicamentos.ConsoleApp.Controllers
{
    [Route("fornecedores")]
    public class ControladorFornecedor : Controller
    {
        private readonly IRepositorioFornecedor repositorioFornecedor;

        public ControladorFornecedor()
        {
            var contexto = new ContextoDados(true);
            repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            return View(new CadastrarFornecedorViewModel());
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(CadastrarFornecedorViewModel viewModel)
        {
            var fornecedor = viewModel.ParaEntidade();

            repositorioFornecedor.CadastrarRegistro(fornecedor);

            return View("Notificacao", new NotificacaoViewModel("Fornecedor Cadastrado!", $"O fornecedor \"{fornecedor.Nome}\" foi cadastrado com sucesso."));
        }

        [HttpGet("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id)
        {
            var fornecedor = repositorioFornecedor.SelecionarRegistroPorId(id);

            if (fornecedor == null)
                return View("Notificacao", new NotificacaoViewModel("Não Encontrado", "Fornecedor não encontrado."));

            var viewModel = new EditarFornecedorViewModel(fornecedor);
            return View(viewModel);
        }

        [HttpPost("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id, EditarFornecedorViewModel viewModel)
        {
            var fornecedorEditado = viewModel.ParaEntidade();

            repositorioFornecedor.EditarRegistro(id, fornecedorEditado);

            return View("Notificacao", new NotificacaoViewModel("Fornecedor Atualizado!", $"O fornecedor \"{fornecedorEditado.Nome}\" foi atualizado com sucesso."));
        }

        [HttpGet("excluir/{id:int}")]
        public IActionResult Excluir([FromRoute] int id)
        {
            var fornecedor = repositorioFornecedor.SelecionarRegistroPorId(id);

            if (fornecedor == null)
                return View("Notificacao", new NotificacaoViewModel("Não Encontrado", "Fornecedor não encontrado."));

            var viewModel = new ExcluirFornecedorViewModel(fornecedor.Id, fornecedor.Nome);
            return View(viewModel);
        }

        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirConfirmado([FromRoute] int id)
        {
            repositorioFornecedor.ExcluirRegistro(id);

            return View("Notificacao", new NotificacaoViewModel("Fornecedor Excluído!", "O fornecedor foi excluído com sucesso."));
        }

        [HttpGet("visualizar")]
        public IActionResult Visualizar()
        {
            var fornecedores = repositorioFornecedor.SelecionarRegistros();
            var viewModel = new VisualizarFornecedoresViewModel(fornecedores);

            return View(viewModel);
        }

        [HttpGet("visualizar/{id:int}")]
        public IActionResult VisualizarPorId([FromRoute] int id)
        {
            var fornecedor = repositorioFornecedor.SelecionarRegistroPorId(id);

            if (fornecedor == null)
                return View("Notificacao", new NotificacaoViewModel("Não Encontrado", $"O fornecedor com ID {id} não foi encontrado."));

            var detalhes = fornecedor.ParaDetalhesVM();
            return View("DetalhesFornecedor", detalhes);
        }
    }
}
