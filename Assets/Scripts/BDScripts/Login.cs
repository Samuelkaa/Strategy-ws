using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : BDConnect
{
    public static new Login Instance;

    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;

    public JSONObject accountInfo;

    private void Awake()
    {
        Instance = this;
    }

    public void LoginButtonClick()
    {
        StartCoroutine(LoginEnum());
    }

    public IEnumerator LoginEnum()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", _login.text);
        form.AddField("password", _password.text);

        UnityWebRequest www = UnityWebRequest.Post(serverip + "strategy/database/login.php", form);
        yield return www.SendWebRequest();


        if (www.downloadHandler.text != "Аккаунт не найден")
        {
            SceneManager.LoadScene("Game");
        }

        accountInfo = new JSONObject(www.downloadHandler.text);
        Debug.Log(accountInfo.ToString());
    }
}
