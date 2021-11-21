using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public static MoneyScript Instance;

    [SerializeField] private GameObject _moneyObject;
    [SerializeField] private Text _moneyAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _moneyAmount.text = Login.Instance.accountInfo.GetField("money").str;
    }

    public IEnumerator AddMoneyOnKill()
    {
        WWWForm form = new WWWForm();
        form.AddField("accountID", Login.Instance.accountInfo.GetField("id").str);
        form.AddField("moneyCount", Random.Range(5, 10));

        UnityWebRequest www = UnityWebRequest.Post(BDConnect.Instance.serverip + "strategy/supplies/moneyAdd.php", form);
        yield return www.SendWebRequest();

        StartCoroutine(GetMoneyAmount());

        Debug.Log(www.downloadHandler.text);
    }

    public IEnumerator GetMoneyAmount()
    {
        WWWForm form = new WWWForm();
        form.AddField("accountID", Login.Instance.accountInfo.GetField("id").str);

        UnityWebRequest www = UnityWebRequest.Post(BDConnect.Instance.serverip + "strategy/supplies/getMoney.php", form);
        yield return www.SendWebRequest();

        _moneyAmount.text = www.downloadHandler.text;
    }
}
