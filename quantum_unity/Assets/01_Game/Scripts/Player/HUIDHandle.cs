using Quantum;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUIDHandle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] Transform anchorHUID;

    public void Setup(string name)
    {
        nameTxt.text = name;
        var parent = GameObject.Find("HUIDparent").GetComponent<Transform>();
        transform.parent = parent;
    }
    private void Update()
    {
        UpdateUIPosition();
    }
    private void UpdateUIPosition()
    {
        var pos = Camera.main.WorldToScreenPoint(anchorHUID.position);
        transform.position = pos;
    }
}
