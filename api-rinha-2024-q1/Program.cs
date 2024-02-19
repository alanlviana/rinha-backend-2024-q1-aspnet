using System.Text.Json.Serialization;
using api_rinha_2024_q1;
using api_rinha_2024_q1.Repositories;
using api_rinha_2024_q1.ViewModels;
var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
  options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

var app = builder.Build();

app.MapPost("/clientes/{codigo_cliente}/transacoes", async (ITransacaoRepository transacaoRepository, int codigo_cliente, NovaTransacaoViewModel novaTransacao) => await new ClientesController(transacaoRepository).AdicionarTransacao(codigo_cliente, novaTransacao));
app.MapGet("/clientes/{codigo_cliente}/extrato", async (ITransacaoRepository transacaoRepository, int codigo_cliente) => await new ClientesController(transacaoRepository).ObterExtrato(codigo_cliente));

app.Run();

[JsonSerializable(typeof(NovaTransacaoViewModel[]))]
[JsonSerializable(typeof(RespostaTransacao[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}