using UnityEngine;
using MySql.Data.MySqlClient;

public class BDConnect : MonoBehaviour
{
    private MySqlConnection sqlConnection = new MySqlConnection("server=localhost; port=3306; username=root; password=; database=strategy_bd");

    public void OpenConnection()
    {
        if (sqlConnection.State == System.Data.ConnectionState.Closed)
            sqlConnection.Open();
    }

    public void CloseConnection()
    {
        if (sqlConnection.State == System.Data.ConnectionState.Open)
            sqlConnection.Close();
    }

    public MySqlConnection ConnectionStatus()
    {
        return sqlConnection;
    }
}
