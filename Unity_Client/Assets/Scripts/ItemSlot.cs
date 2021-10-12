using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public GameObject icon;

    public void UpdateSlot()
    {
        if (ItemManager.instance.items[transform.GetSiblingIndex()] != null)
        {
            icon.GetComponent<Image>().sprite = ItemManager.instance.items[transform.GetSiblingIndex()].itemSprite;
            icon.SetActive(true);
        }
        else
        {
            icon.SetActive(false);
        }
    }
}
