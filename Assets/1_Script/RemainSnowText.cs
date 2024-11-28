using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainSnowText : MonoBehaviour
{
    public TextMeshProUGUI tmpUgui;

    void FixedUpdate()
    {
        tmpUgui.text = string.Format("{0}/{1}", GameManager.Instance.TotalItemCount, GameManager.Instance.RemainItemCount);
    }
}
