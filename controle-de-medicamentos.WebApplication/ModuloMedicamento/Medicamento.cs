﻿using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Medicamento() { }
        public List<string> SintomasTratados { get; set; }
        public Medicamento(string nome, string descricao, int quantidade, decimal valor, Fornecedor fornecedor, List<string> sintomasTratados)
        {
            Nome = nome;
            Descricao = descricao;
            Quantidade = quantidade;
            Valor = valor;
            Fornecedor = fornecedor;
            SintomasTratados = sintomasTratados;
        }

        public override void AtualizarRegistro(Medicamento registroEditado)
        {
            Nome = registroEditado.Nome;
            Descricao = registroEditado.Descricao;
            Quantidade = registroEditado.Quantidade;
            Valor = registroEditado.Valor;
            Fornecedor = registroEditado.Fornecedor;
        }


        public override string Validar()
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
                erros.Add("O nome deve conter entre 3 e 100 caracteres.");

            if (string.IsNullOrWhiteSpace(Descricao) || Descricao.Length < 5 || Descricao.Length > 255)
                erros.Add("A descrição deve conter entre 5 e 255 caracteres.");

            if (Quantidade < 0)
                erros.Add("A quantidade não pode ser negativa.");

            if (Fornecedor == null)
                erros.Add("Um fornecedor válido deve ser selecionado.");
            if (Valor <= 0)
                erros.Add("O valor deve ser maior que zero.");
            return string.Join(Environment.NewLine, erros);
        }

        public string ObterStatusEstoque()
        {
            return Quantidade < 20 ? "Em falta" : "Disponível";
        }
    }
}