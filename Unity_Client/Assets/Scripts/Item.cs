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

    [SerializeField]
    public Sprite itemSprite;

    [SerializeField]
    public int itemID;

    [SerializeField]
    public string itemName;

    [SerializeField]
    public int price;

    [SerializeField]
    public ItemType itemType;

    [SerializeField]
    public ItemSource itemSource;

	public Sprite getItemSprite() {
		return this.itemSprite;
	}

	public void setItemSprite(Sprite itemSprite) {
		this.itemSprite = itemSprite;
	}

	public int getItemID() {
		return this.itemID;
	}

	public void setItemID(int itemID) {
		this.itemID = itemID;
	}

	public string getItemName() {
		return this.itemName;
	}

	public void setItemName(string itemName) {
		this.itemName = itemName;
	}

	public int getPrice() {
		return this.price;
	}

	public void setPrice(int price) {
		this.price = price;
	}

	public ItemType getItemType() {
		return this.itemType;
	}

	public void setItemType(ItemType itemType) {
		this.itemType = itemType;
	}

	public ItemSource getItemSource() {
		return this.itemSource;
	}

	public void setItemSource(ItemSource itemSource) {
		this.itemSource = itemSource;
	}


    public string ToJSON(){
        return JsonUtility.ToJson(this);
    }
}
