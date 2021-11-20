using UnityEngine;
using UnityEngine.UI;
using System;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] private GameObject _moneyObject;
    [SerializeField] private Text _moneyAmount;

    private void Start()
    {
        _moneyAmount.text = Login.Instance.accountInfo.GetField("money").str;
    }

    public void AddMoneyOnKill()
    {

    }
}
