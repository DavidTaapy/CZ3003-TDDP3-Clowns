using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemSource
{
    Shop,
    Character,
    Leaderboard
}

[System.Serializable]
public class Item
{
    public Sprite itemSprite;
    public int itemID;
    public string itemName;
    public int itemPrice;
    public ItemSource itemSource;
    public int itemCount;
    public string itemDescription;
}
