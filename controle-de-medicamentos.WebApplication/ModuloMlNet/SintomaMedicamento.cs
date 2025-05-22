using Microsoft.ML.Data;

namespace controle_de_medicamentos.WebApplication.ModuloMlNet
{
    public class SintomaMedicamento
    {
        [LoadColumn(0)]
        public string Sintomas { get; set; }

        [LoadColumn(1)]
        public string MedicamentoRecomendado { get; set; }
    }

    public class SintomaPredicao
    {
        [ColumnName("PredictedLabel")]
        public string MedicamentoRecomendado { get; set; }

        public float[] Score { get; set; }
    }
}
