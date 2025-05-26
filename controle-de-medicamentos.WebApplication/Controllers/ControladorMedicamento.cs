using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using ControleDeMedicamentos.ConsoleApp.Extensoes;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeMedicamentos.ConsoleApp.Controllers
{
    [Route("medicamentos")]
    public class ControladorMedicamento : Controller
    {
        private readonly IRepositorioMedicamento repositorioMedicamento;
        private readonly IRepositorioFornecedor repositorioFornecedor;

        public ControladorMedicamento()
        {
            var contexto = new ContextoDados(true);
            repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
            repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var fornecedores = repositorioFornecedor.SelecionarRegistros();
            var viewModel = new CadastrarMedicamentoViewModel(fornecedores);

            return View(viewModel);
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(CadastrarMedicamentoViewModel viewModel)
        {
            var fornecedores = repositorioFornecedor.SelecionarRegistros();
            var medicamento = viewModel.ParaEntidade(fornecedores);

            repositorioMedicamento.CadastrarRegistro(medicamento);

            var notificacao = new NotificacaoViewModel("Medicamento Cadastrado!",
                $"O medicamento \"{medicamento.Nome}\" foi cadastrado com sucesso.");

            return View("Notificacao", notificacao);
        }

        [HttpGet("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id)
        {
            var medicamento = repositorioMedicamento.SelecionarRegistroPorId(id);
            var fornecedores = repositorioFornecedor.SelecionarRegistros();

            if (medicamento == null)
                return View("Notificacao", new NotificacaoViewModel("Não Encontrado", "Medicamento não encontrado."));

            var viewModel = new EditarMedicamentoViewModel(medicamento, fornecedores);
            return View(viewModel);
        }

        [HttpPost("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id, EditarMedicamentoViewModel viewModel)
        {
            var fornecedores = repositorioFornecedor.SelecionarRegistros();
            var medicamentoEditado = viewModel.ParaEntidade(fornecedores);

            repositorioMedicamento.EditarRegistro(id, medicamentoEditado);

            return View("Notificacao", new NotificacaoViewModel("Medicamento Editado!",
                $"O medicamento \"{medicamentoEditado.Nome}\" foi atualizado com sucesso."));
        }

        [HttpGet("excluir/{id:int}")]
        public IActionResult Excluir([FromRoute] int id)
        {
            var medicamento = repositorioMedicamento.SelecionarRegistroPorId(id);

            if (medicamento == null)
                return View("Notificacao", new NotificacaoViewModel("Não Encontrado", "Medicamento não encontrado."));

            var viewModel = new ExcluirMedicamentoViewModel(medicamento.Id, medicamento.Nome);
            return View(viewModel);
        }

        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirConfirmado([FromRoute] int id)
        {
            repositorioMedicamento.ExcluirRegistro(id);

            return View("Notificacao", new NotificacaoViewModel("Medicamento Excluído!", "Registro excluído com sucesso."));
        }

        [HttpGet("visualizar")]
        public IActionResult Visualizar()
        {
            var medicamentos = repositorioMedicamento.SelecionarRegistros();
            var viewModel = new VisualizarMedicamentosViewModel(medicamentos);

            return View(viewModel);
        }

        [HttpGet("visualizar/{id:int}")]
        public IActionResult VisualizarPorId([FromRoute] int id)
        {
            var medicamento = repositorioMedicamento.SelecionarRegistroPorId(id);

            if (medicamento == null)
                return View("Notificacao", new NotificacaoViewModel("Não Encontrado", $"O medicamento com ID {id} não foi encontrado."));

            var detalhes = medicamento.ParaDetalhesVM();
            return View("DetalhesMedicamento", detalhes);
        }

        [HttpGet("adicionar-estoque/{id:int}")]
        public IActionResult AdicionarEstoque([FromRoute] int id)
        {
            var medicamento = repositorioMedicamento.SelecionarRegistroPorId(id);

            if (medicamento == null)
                return View("Notificacao", new NotificacaoViewModel("Não Encontrado", "Medicamento não encontrado."));

            var viewModel = new AdicionarEstoqueViewModel(medicamento);
            return View(viewModel);
        }

        [HttpPost("adicionar-estoque/{id:int}")]
        public IActionResult AdicionarEstoque([FromRoute] int id, AdicionarEstoqueViewModel viewModel)
        {
            var medicamento = repositorioMedicamento.SelecionarRegistroPorId(id);

            if (medicamento == null)
                return View("Notificacao", new NotificacaoViewModel("Erro", "Medicamento não encontrado."));

            medicamento.Quantidade += viewModel.QuantidadeAdicionar;
            repositorioMedicamento.EditarRegistro(id, medicamento);

            return View("Notificacao", new NotificacaoViewModel("Estoque Atualizado!", $"Estoque do medicamento \"{medicamento.Nome}\" atualizado com sucesso."));
        }
    }
}
