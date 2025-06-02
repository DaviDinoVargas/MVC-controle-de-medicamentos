using ControleDeMedicamentos.ConsoleApp.ControleDeMedicamentos.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class RepositorioMedicamentoEmMemoria : IRepositorioMedicamento
    {
        private List<Medicamento> medicamentos = new List<Medicamento>();
        private int contadorIds = 0;

        public void CadastrarRegistro(Medicamento novoRegistro)
        {
            var existente = medicamentos.FirstOrDefault(m =>
                m.Nome == novoRegistro.Nome &&
                m.Fornecedor.Id == novoRegistro.Fornecedor.Id);

            if (existente != null)
            {
                existente.Quantidade += novoRegistro.Quantidade;
                return;
            }

            novoRegistro.Id = ++contadorIds;
            medicamentos.Add(novoRegistro);
        }

        public bool EditarRegistro(int idRegistro, Medicamento registroEditado)
        {
            Medicamento medicamentoSelecionado = SelecionarRegistroPorId(idRegistro);

            if (medicamentoSelecionado == null)
                return false;

            medicamentoSelecionado.AtualizarRegistro(registroEditado);

            return true;
        }

        public bool ExcluirRegistro(int idRegistro)
        {
            Medicamento medicamentoSelecionado = SelecionarRegistroPorId(idRegistro);

            if (medicamentoSelecionado == null)
                return false;

            medicamentos.Remove(medicamentoSelecionado);

            return true;
        }
        public bool ExcluirRegistros(List<int> ids)
        {
            int quantidadeOriginal = medicamentos.Count;

            medicamentos.RemoveAll(m => ids.Contains(m.Id));

            return medicamentos.Count < quantidadeOriginal; 
        }
        public Medicamento SelecionarRegistroPorId(int idRegistro)
        {
            return medicamentos.FirstOrDefault(m => m.Id == idRegistro);
        }

        public List<Medicamento> SelecionarRegistros()
        {
            return medicamentos;
        }
    }
}