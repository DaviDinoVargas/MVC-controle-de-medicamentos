namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor
{
    public class CadastrarFornecedorViewModel
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }
    }

    public class EditarFornecedorViewModel : CadastrarFornecedorViewModel
    {
        public int Id { get; set; }

        public EditarFornecedorViewModel() { }

        public EditarFornecedorViewModel(Fornecedor fornecedor)
        {
            Id = fornecedor.Id;
            Nome = fornecedor.Nome;
            Telefone = fornecedor.Telefone;
            CNPJ = fornecedor.CNPJ;
        }
    }

    public class ExcluirFornecedorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ExcluirFornecedorViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class DetalhesFornecedorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }

        public DetalhesFornecedorViewModel(Fornecedor fornecedor)
        {
            Id = fornecedor.Id;
            Nome = fornecedor.Nome;
            Telefone = fornecedor.Telefone;
            CNPJ = fornecedor.CNPJ;
        }
    }

    public class VisualizarFornecedoresViewModel
    {
        public List<DetalhesFornecedorViewModel> Registros { get; set; }

        public VisualizarFornecedoresViewModel(List<Fornecedor> fornecedores)
        {
            Registros = fornecedores.Select(f => new DetalhesFornecedorViewModel(f)).ToList();
        }
    }
}
