using System.Collections.Generic;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public static class FornecedorExtensions
    {
        public static Fornecedor ParaEntidade(this CadastrarFornecedorViewModel viewModel)
        {
            return new Fornecedor(viewModel.Nome, viewModel.Telefone, viewModel.CNPJ);
        }

        public static Fornecedor ParaEntidade(this EditarFornecedorViewModel viewModel)
        {
            return new Fornecedor(viewModel.Nome, viewModel.Telefone, viewModel.CNPJ);
        }

        public static DetalhesFornecedorViewModel ParaDetalhesVM(this Fornecedor fornecedor)
        {
            return new DetalhesFornecedorViewModel(fornecedor);
        }
    }
}
