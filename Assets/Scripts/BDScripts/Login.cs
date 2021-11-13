using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Data;

public class Login : BDConnect
{
    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;

    public string authorizedLogin;
    public string authorizedPassword;

    public static Login Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void LoginButtonClick()
    {
        if (_login.text == "" || _password.text == "")
        {
            Debug.Log("Введены не все данные");
            return;
        }

        BDConnect bd = new BDConnect();

        DataTable dataTable = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @userLogin AND `password` = @userPassword", bd.ConnectionStatus());
        command.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = _login.text;
        command.Parameters.Add("@userPassword", MySqlDbType.VarChar).Value = _password.text;

        adapter.SelectCommand = command;
        adapter.Fill(dataTable);

        if (dataTable.Rows.Count > 0)
        {
            Debug.Log("Аккаунт найден");
            authorizedLogin = _login.text;
            authorizedPassword = _password.text;
            MenuManager.Instance.ChangeState(MenuState.StartGame);
        }
        else
        {
            Debug.Log("Неверное сочетание логина и пароля");
        }
    }
}
