using System.Text.Json.Serialization;

namespace api_rinha_2024_q1.ViewModels;

public class NovaTransacaoViewModel
{
    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }

    [JsonPropertyName("tipo")]
    public string Tipo { get; set; }

    [JsonPropertyName("descricao")]
    public string? Descricao { get; set; }

}