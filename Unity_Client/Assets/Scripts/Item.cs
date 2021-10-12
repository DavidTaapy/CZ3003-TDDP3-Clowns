using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Powerup,
        Skin
    }

    public enum ItemSource
    {
        Shop,
        Character,
        Leaderboard
    }

    public Sprite itemSprite;
    public int itemID;
    public string itemName;
    public int price;
    public ItemType itemType;
    public ItemSource itemSource;
}
