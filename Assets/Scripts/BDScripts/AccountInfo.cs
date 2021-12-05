using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ServerJSON
{
    public AccountInfo data;
}

[Serializable]
public class AccountInfo
{
    public int id;
    public string login;
    public string password;
    public string nickname;
}
