using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainSnowText : MonoBehaviour
{
    public TextMeshProUGUI tmpUgui;

    void FixedUpdate()
    {
        tmpUgui.text = string.Format("x{0}", GameManager.Instance.RemainItemCount);
    }
}
