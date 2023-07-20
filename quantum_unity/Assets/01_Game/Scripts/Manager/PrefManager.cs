using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefManager
{
    public static string PlayerName
    {
        get => PlayerPrefs.GetString("PlayerName", "Name");
        set => PlayerPrefs.SetString("PlayerName", value);
    }
}
