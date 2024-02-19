using System.Runtime.Serialization;

namespace api_rinha_2024_q1.Exceptions;


public class ClienteNaoEncontradoException: Exception{
    public ClienteNaoEncontradoException()
    {
    }

    public ClienteNaoEncontradoException(string? message) : base(message)
    {
    }

    public ClienteNaoEncontradoException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}