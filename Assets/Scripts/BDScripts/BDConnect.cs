using UnityEngine;

public class BDConnect : MonoBehaviour
{
    public static BDConnect Instance;
    public string serverip = "http://localhost/";

    private void Awake()
    {
        Instance = this;
    }
}
