using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Data;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] private GameObject _moneyObject;
    [SerializeField] private Text _moneyAmount;
    public static MoneyScript Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetMoneyInfoOnStart();
    }

    public void GetMoneyInfoOnStart()
    {
        _moneyObject.SetActive(true);

        BDConnect bd = new BDConnect();

        MySqlDataReader mysql_result;
        MySqlCommand command = new MySqlCommand("SELECT `money` FROM `users` WHERE `users`.`login` = @userLogin", bd.ConnectionStatus()); // Стоит заменить логин на id
        command.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = Login.Instance.authorizedLogin;
        bd.OpenConnection();
        mysql_result = command.ExecuteReader();
        if (mysql_result.Read())
        {
            _moneyAmount.text = mysql_result.GetString(0);
        }
        else
        {
            _moneyAmount.text = "???";
        }
        bd.CloseConnection();
    }

    public void GetMoneyAmount()
    {
        BDConnect bd = new BDConnect();

        MySqlDataReader mysql_result;
        MySqlCommand command = new MySqlCommand("SELECT `money` FROM `users` WHERE `users`.`login` = @userLogin", bd.ConnectionStatus());
        command.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = Login.Instance.authorizedLogin;

        bd.OpenConnection();
        mysql_result = command.ExecuteReader();
        if (mysql_result.Read())
        {
            _moneyAmount.text = mysql_result.GetString(0);
        }
        else
        {
            _moneyAmount.text = "???";
        }
        bd.CloseConnection();
    }

    public void AddMoney(int money)
    {
        BDConnect bd = new BDConnect();

        MySqlDataReader mysql_result;
        MySqlCommand command = new MySqlCommand("UPDATE `users` SET `money` = @moneyAdd WHERE `users`.`login` = @userLogin;", bd.ConnectionStatus());
        command.Parameters.Add("@moneyAdd", MySqlDbType.Int32).Value = money;
        command.Parameters.Add("userLogin", MySqlDbType.VarChar).Value = Login.Instance.authorizedLogin;

        bd.OpenConnection();
        mysql_result = command.ExecuteReader();
        if (mysql_result.Read())
        {
            _moneyAmount.text = mysql_result.GetString(0);
        }
        else
        {
            _moneyAmount.text = "???";
        }
    }
}
