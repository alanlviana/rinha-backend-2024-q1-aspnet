using System.Runtime.Serialization;

namespace api_rinha_2024_q1.Exceptions;

internal class LimiteIndisponivelException : Exception
{
    public LimiteIndisponivelException()
    {
    }

    public LimiteIndisponivelException(string? message) : base(message)
    {
    }

    public LimiteIndisponivelException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}