using System.Data;
using api_rinha_2024_q1.Exceptions;
using api_rinha_2024_q1.Repositories;
using api_rinha_2024_q1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace api_rinha_2024_q1;

public class ClientesController
{
    private const string IDENTIFICADOR_CREDITO = "c";
    private const string IDENTIFICADOR_DEBITO = "d";
    private const int TAMANHO_MAXIMO_DESCRICAO = 10;
    private readonly ITransacaoRepository _transacaoRepository;

    public ClientesController(ITransacaoRepository transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<IResult> AdicionarTransacao(int codigo_cliente, NovaTransacaoViewModel novaTransacao)
    {

        
        if (string.IsNullOrEmpty(novaTransacao.Descricao) || novaTransacao.Descricao.Length > TAMANHO_MAXIMO_DESCRICAO)
        {
            return Results.UnprocessableEntity();
        }

        if (!IsInteger(novaTransacao.Valor)){
            return Results.UnprocessableEntity();
        }

        if (novaTransacao.Tipo != IDENTIFICADOR_CREDITO && novaTransacao.Tipo != IDENTIFICADOR_DEBITO)
        {
            return Results.UnprocessableEntity();
        }

        try
        {

            var resultado = await _transacaoRepository.AdicionarTransacao(codigo_cliente, novaTransacao.Tipo, novaTransacao.Descricao, (long)novaTransacao.Valor);

            return Results.Ok(new RespostaTransacao()
            {
                 Saldo = (long)resultado.NovoSaldo,
                 Limite = (long)resultado.Limite
            });
            //return Results.Content("{}", "application/json");
        }
        catch (LimiteIndisponivelException e)
        {
            return Results.UnprocessableEntity();
        }
        catch (ClienteNaoEncontradoException e)
        {
            return Results.NotFound();
        }

    }

    private bool IsInteger(decimal valor)
    {
        return valor == Math.Truncate(valor);
    }

    public async Task<IResult> ObterExtrato(int codigo_cliente)
    {
        try
        {
            var resultado = await _transacaoRepository.ObterExtrato(codigo_cliente);
            return Results.Content(resultado, "application/json");
        }
        catch (ClienteNaoEncontradoException e)
        {
            return Results.NotFound();
        }

    }
}



