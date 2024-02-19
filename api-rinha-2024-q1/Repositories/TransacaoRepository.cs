using System.Data;
using System.Runtime.Serialization;
using api_rinha_2024_q1.Exceptions;
using Npgsql;
using Npgsql.Internal;

namespace api_rinha_2024_q1.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    public readonly string EXECUTAR_PROCEDURE_REALIZA_TRANSACAO = "SELECT * FROM realizar_transacao($1, $2, $3, $4);";
    public readonly string EXECUTAR_PROCEDURE_EXTRATO = "select * from obter_saldo_e_transacoes($1);";

    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
    public TransacaoRepository(IDatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    public async Task<(decimal NovoSaldo, decimal Limite)> AdicionarTransacao(int codigoCliente, string tipo, string descricao, long valor)
    {
        try{
            using var connection = _databaseConnectionFactory.GetConnection();
            using var command = new NpgsqlCommand(EXECUTAR_PROCEDURE_REALIZA_TRANSACAO, connection){Parameters = {
                new() { Value = codigoCliente },
                new() { Value = tipo },
                new() { Value = descricao },
                new() { Value = valor }
            }};

            await command.PrepareAsync();
            var reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();
            return (reader.GetDecimal(0), reader.GetDecimal(1));
        }catch(PostgresException e){
            if (e.MessageText.StartsWith("RN01")){
                throw new ClienteNaoEncontradoException(e.MessageText.Substring(5));
            }else if(e.MessageText.StartsWith("RN02")){
                throw new LimiteIndisponivelException(e.MessageText.Substring(5));
            }else{
                throw;
            }
        }
    }

    public async Task<string> ObterExtrato(int codigo_cliente)
    {
        try{
            using var connection = _databaseConnectionFactory.GetConnection();
            using var command = new NpgsqlCommand(EXECUTAR_PROCEDURE_EXTRATO, connection){Parameters = {
                new() { Value = codigo_cliente },
            }};
            await command.PrepareAsync();
            var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            return reader.GetString(0);
        }catch(PostgresException e){
            if (e.MessageText.StartsWith("RN01")){
                throw new ClienteNaoEncontradoException(e.MessageText.Substring(5));
            }else{
                throw;
            }
        }
    }
}

public class RetornoProcedureRealizaTransacao{
    public long novo_saldo { get; set; }
    public long limite_cliente { get; set; }
}

public class RetornoProcedureExtrato{
    public string json_extrato { get; set; }
}



