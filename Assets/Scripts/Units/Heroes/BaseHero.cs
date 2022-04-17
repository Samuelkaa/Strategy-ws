using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BaseHero : BaseUnit
{
    [SerializeField] private string nickname;

    private void Start()
    {
        nickname = Login.Instance.accountInfo.GetField("name").str;
        UnitName = nickname;
    }
}
