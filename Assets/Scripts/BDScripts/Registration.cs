using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Data;

public class Registration : MonoBehaviour
{

    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;
    [SerializeField] private InputField _name;

    public static Registration Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterButtonClick()
    {
        if (_login.text == "" || _password.text == "" || _name.text == "")
        {
            Debug.Log("Введены не все данные");
            return;
        }

        if (CheckLoginExist())
        {
            return;
        }

        if (CheckNameExist())
        {
            return;
        }

        BDConnect bdConnect = new BDConnect();

        MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `name`) VALUES (@userLogin, @userPassword, @userName)", bdConnect.ConnectionStatus());
        command.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = _login.text;
        command.Parameters.Add("@userPassword", MySqlDbType.VarChar).Value = _password.text;
        command.Parameters.Add("@userName", MySqlDbType.VarChar).Value = _name.text;

        bdConnect.OpenConnection();

        if (command.ExecuteNonQuery() == 1)
        {
            Debug.Log("Персонаж создан");
            MenuManager.Instance.ChangeState(MenuState.Login);
        }
        else
        {
            Debug.Log("Чета мусорка какая-то");
        }

        bdConnect.CloseConnection();
    }

    public bool CheckLoginExist()
    {
        BDConnect bd = new BDConnect();

        DataTable dataTable = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @userLogin", bd.ConnectionStatus());
        command.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = _login.text;

        adapter.SelectCommand = command;
        adapter.Fill(dataTable);

        if (dataTable.Rows.Count > 0)
        {
            Debug.Log("Логин занят");
            return true;
        }
        else
        {
            Debug.Log("Login not exist, next");
            return false;
        }
    }

    public bool CheckNameExist()
    {
        BDConnect bd = new BDConnect();

        DataTable dataTable = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `name` = @userName", bd.ConnectionStatus());
        command.Parameters.Add("@userName", MySqlDbType.VarChar).Value = _name.text;

        adapter.SelectCommand = command;
        adapter.Fill(dataTable);

        if (dataTable.Rows.Count > 0)
        {
            Debug.Log("Никнейм занят");
            return true;
        }
        else
        {
            Debug.Log("Nickname not exist, next");
            return false;
        }
    }
}
