using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILobby : MonoBehaviour
{
    [SerializeField] TMP_InputField nameLb;
    private void Start()
    {
        nameLb.text = PrefManager.PlayerName;
    }
    public void SetName()
    {
        if(nameLb.text != null)
            PrefManager.PlayerName = nameLb.text;
    }
}
