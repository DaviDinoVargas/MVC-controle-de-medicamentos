using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeMedicamentos.ConsoleApp.Extensoes
{
    public static class MedicamentoExtensions
    {
        public static Medicamento ParaEntidade(this CadastrarMedicamentoViewModel viewModel, List<Fornecedor> fornecedores)
        {
            var fornecedor = fornecedores.FirstOrDefault(f => f.Id == viewModel.FornecedorId);

            return new Medicamento(
                viewModel.Nome,
                viewModel.Descricao,
                viewModel.Quantidade,
                viewModel.Valor,
                fornecedor,
                viewModel.SintomasTratados ?? new List<string>()
            );
        }

        public static Medicamento ParaEntidade(this EditarMedicamentoViewModel viewModel, List<Fornecedor> fornecedores)
        {
            var fornecedor = fornecedores.FirstOrDefault(f => f.Id == viewModel.FornecedorId);

            return new Medicamento(
                viewModel.Nome,
                viewModel.Descricao,
                viewModel.Quantidade,
                viewModel.Valor,
                fornecedor,
                viewModel.SintomasTratados ?? new List<string>()
            );
        }

        public static DetalhesMedicamentoViewModel ParaDetalhesVM(this Medicamento medicamento)
        {
            return new DetalhesMedicamentoViewModel(medicamento);
        }
    }
}
