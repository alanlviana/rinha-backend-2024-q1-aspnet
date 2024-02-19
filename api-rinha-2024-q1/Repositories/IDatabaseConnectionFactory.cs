using System.Data;
using Npgsql;

namespace api_rinha_2024_q1.Repositories;

public interface IDatabaseConnectionFactory{
    NpgsqlConnection GetConnection();
}