using UnityEngine;

public class BDConnect : MonoBehaviour
{
    public static BDConnect Instance;
    public string serverip = "http://89.108.77.208/";

    private void Awake()
    {
        Instance = this;
    }
}
