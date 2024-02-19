using System.Data;
using Npgsql;

namespace api_rinha_2024_q1.Repositories;

public class DatabaseConnectionFactory: IDatabaseConnectionFactory{

    private readonly NpgsqlDataSource _dataSource;

    public DatabaseConnectionFactory()
    {
        var host = Environment.GetEnvironmentVariable("DB_HOSTNAME") ?? "127.0.0.1";
        var conectionString = $"Host={host};Database=rinha;Username=admin;Password=123";
        _dataSource = new NpgsqlSlimDataSourceBuilder(conectionString).Build();
    }

    public NpgsqlConnection GetConnection(){
        var connection = _dataSource.OpenConnection();
        return connection;
    }
}