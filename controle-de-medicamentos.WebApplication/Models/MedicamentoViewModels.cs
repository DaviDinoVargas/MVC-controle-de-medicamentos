using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class CadastrarMedicamentoViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public int FornecedorId { get; set; }
        public List<string> SintomasTratados { get; set; }

        public List<SelecionarFornecedorViewModel> FornecedoresDisponiveis { get; set; }

        public CadastrarMedicamentoViewModel() { }

        public CadastrarMedicamentoViewModel(List<Fornecedor> fornecedores)
        {
            FornecedoresDisponiveis = fornecedores
                .Select(f => new SelecionarFornecedorViewModel(f.Id, f.Nome)).ToList();

            SintomasTratados = new List<string>();
        }
    }

    public class EditarMedicamentoViewModel : CadastrarMedicamentoViewModel
    {
        public int Id { get; set; }

        public EditarMedicamentoViewModel() { }

        public EditarMedicamentoViewModel(Medicamento medicamento, List<Fornecedor> fornecedores)
            : base(fornecedores)
        {
            Id = medicamento.Id;
            Nome = medicamento.Nome;
            Descricao = medicamento.Descricao;
            Quantidade = medicamento.Quantidade;
            Valor = medicamento.Valor;
            FornecedorId = medicamento.Fornecedor?.Id ?? 0;
            SintomasTratados = medicamento.SintomasTratados ?? new List<string>();
        }
    }

    public class ExcluirMedicamentoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ExcluirMedicamentoViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class VisualizarMedicamentosViewModel
    {
        public List<DetalhesMedicamentoViewModel> Registros { get; set; }

        public VisualizarMedicamentosViewModel(List<Medicamento> medicamentos)
        {
            Registros = medicamentos.Select(m => new DetalhesMedicamentoViewModel(m)).ToList();
        }
    }

    public class DetalhesMedicamentoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string StatusEstoque { get; set; }
        public string NomeFornecedor { get; set; }
        public List<string> SintomasTratados { get; set; }

        public DetalhesMedicamentoViewModel(Medicamento medicamento)
        {
            Id = medicamento.Id;
            Nome = medicamento.Nome;
            Descricao = medicamento.Descricao;
            Quantidade = medicamento.Quantidade;
            Valor = medicamento.Valor;
            StatusEstoque = medicamento.ObterStatusEstoque();
            NomeFornecedor = medicamento.Fornecedor?.Nome ?? "N/A";
            SintomasTratados = medicamento.SintomasTratados ?? new List<string>();
        }
    }

    public class AdicionarEstoqueViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QuantidadeAtual { get; set; }
        public int QuantidadeAdicionar { get; set; }

        public AdicionarEstoqueViewModel() { }

        public AdicionarEstoqueViewModel(Medicamento medicamento)
        {
            Id = medicamento.Id;
            Nome = medicamento.Nome;
            QuantidadeAtual = medicamento.Quantidade;
        }
    }

    public class SelecionarFornecedorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public SelecionarFornecedorViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
