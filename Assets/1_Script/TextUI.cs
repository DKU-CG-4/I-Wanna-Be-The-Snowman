using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    public TextMeshProUGUI tmpUgui;

    public enum ItemType { Snow, Heart };
    public ItemType type;

    void FixedUpdate()
    {
        switch(type)
        {
            case ItemType.Snow:
                tmpUgui.text = string.Format("x{0}", GameManager.Instance.RemainItemCount);
                break;
            case ItemType.Heart:
                tmpUgui.text = string.Format("{0:D2}:{1:D2}", GameManager.Instance.jumpMin, GameManager.Instance.jumpSec);
                break;
        }
    }
}