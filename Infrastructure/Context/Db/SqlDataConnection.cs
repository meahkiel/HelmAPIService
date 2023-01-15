using System.Data.SqlClient;

namespace  Infrastructure.Context.Db;

public class SqlDataConnection : IDisposable
{

    private readonly SqlConnection _sqlConnection;


    public SqlDataConnection(string connectionString){
        _sqlConnection = new SqlConnection(connectionString);
    }

    public void Open() {
        _sqlConnection.Open();
    }

    public void Close() {
        _sqlConnection.Close();
    }

    public SqlConnection GetConnection() {
        return _sqlConnection;
    }

   

    public void Dispose()
    {
        _sqlConnection.Dispose();
    }
}