using System.Text.Json.Serialization;

namespace api_rinha_2024_q1.ViewModels;
public class RespostaTransacao{
    [JsonPropertyName("limite")]
    public long Limite { get; set; }
    [JsonPropertyName("saldo")]
    public long Saldo { get; set; }
}