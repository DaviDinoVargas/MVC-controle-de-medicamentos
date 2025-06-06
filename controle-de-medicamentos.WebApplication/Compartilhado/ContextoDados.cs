﻿using System.Text.Json.Serialization;
using System.Text.Json;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

public class ContextoDados
{
    //public List<Entidade> Entidade { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    public List<Fornecedor> Fornecedores { get; set; }



    private string pastaArmazenamento = "C:\\temp";
    private string arquivoArmazenamento = "dados.json";

    public ContextoDados()
    {
        //nomeEntidade = new List<Entidade>();
        Medicamentos = new List<Medicamento>();
        Fornecedores = new List<Fornecedor>();
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento))
            Directory.CreateDirectory(pastaArmazenamento);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(json, jsonOptions)!;

        if (contextoArmazenado == null) return;

        //this.Entidade = contextoArmazenado.Entidade;
        this.Medicamentos = contextoArmazenado.Medicamentos;
        this.Fornecedores = contextoArmazenado.Fornecedores;

    }
}
