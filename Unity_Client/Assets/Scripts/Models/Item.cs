using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
        Powerup,
        Accessory
}

public enum ItemSource
{
	Shop,
	Character,
	Leaderboard
}

[System.Serializable]
public class Item
{
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

	[SerializeField]
	public int itemCount;

    [SerializeField]
    public string itemDescription;

	[SerializeField]
    public string spriteSource;

    public string getSpriteSource()
    {
        return this.spriteSource;
    }

    public void setSpriteSource(string spriteSource)
    {
        this.spriteSource = spriteSource;
    }

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

	public int getItemCount() {
		return this.itemCount;
	}

	public void setItemCount(int itemCount) {
		this.itemCount = itemCount;
	}

	public string getItemDescription() {
		return this.itemDescription;
	}

	public void setItemDescription(string itemDescription) {
		this.itemDescription = itemDescription;
	}

    public string ToJSON(){
        return JsonUtility.ToJson(this);
    }
}
