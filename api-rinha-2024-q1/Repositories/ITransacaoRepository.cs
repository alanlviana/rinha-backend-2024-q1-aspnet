namespace api_rinha_2024_q1.Repositories;

public interface ITransacaoRepository{
    Task<(decimal NovoSaldo, decimal Limite)> AdicionarTransacao(int codigoCliente, string tipo, string descricao, long valor);
    Task<string> ObterExtrato(int codigo_cliente);
}