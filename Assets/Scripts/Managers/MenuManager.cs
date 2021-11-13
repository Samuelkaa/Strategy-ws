using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _loginWindow;
    [SerializeField] private GameObject _regWindow;

    public static MenuManager Instance;
    public MenuState MenuState;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        ChangeState(MenuState.Login);
    }

    public void OpenLogin()
    {
        _loginWindow.SetActive(true);
        _regWindow.SetActive(false);
    }

    public void OpenRegistration()
    {
        _loginWindow.SetActive(false);
        _regWindow.SetActive(true);
    }

    public void RegLogSwap(bool isLogin)
    {
        if (isLogin)
        {
            OpenLogin();
        }
        else
        {
            OpenRegistration();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ChangeState(MenuState newState)
    {
        MenuState = newState;
        switch (newState)
        {
            case MenuState.Registration:
                OpenRegistration();
                break;
            case MenuState.Login:
                OpenLogin();
                break;
            case MenuState.StartGame:
                GameStart();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum MenuState
{
    Registration = 0,
    Login = 1,
    StartGame = 2
}
