using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{

    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;
    [SerializeField] private InputField _name;

    public void RegButtonClick()
    {
        StartCoroutine(RegEnum());
    }

    public IEnumerator RegEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", _login.text);
        form.AddField("password", _password.text);
        form.AddField("name", _name.text);

        UnityWebRequest www = UnityWebRequest.Post(BDConnect.Instance.serverip + "strategy/database/registration.php", form);
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log("Ошибка " + www.error);
            yield break;
        }
        else
        {
            MenuManager.Instance.RegLogSwap(false);
        }

        Debug.Log(www.downloadHandler.text);
    }
}
