using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Data;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] private GameObject _moneyObject;
    [SerializeField] private Text _moneyAmount;

    private void Start()
    {
        GetMoneyInfoOnStart();
    }

    public void GetMoneyInfoOnStart()
    {
        _moneyObject.SetActive(true);

        BDConnect bd = new BDConnect();

        DataTable dataTable = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

    }
}
