using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class User
{
    [SerializeField]
    private string id;

    [SerializeField]
    private string userName;

    [SerializeField]
    private int primaryLevel;

    [SerializeField]
    private int eloRating;

    [SerializeField]
    private List<Item> inventory;

    [SerializeField]
    private Character character;
    
    [SerializeField]
    private int points;

    public User(string username, int eloRating, int primaryLevel){
        this.userName = username;
        this.eloRating = eloRating;
        this.primaryLevel = primaryLevel;
        this.inventory = new List<Item>();
        this.points = 0;
    }

    public string ToJSON(){
        return JsonUtility.ToJson(this);
    }

    public int getEloRating()
    {
        return this.eloRating;
    }

    public void setEloRating(int eloRating)
    {
        this.eloRating = eloRating;
    }

    public string getId()
    {
        return this.id;
    }

    public void setId(string id)
    {
        this.id = id;
    }

    public string getUserName()
    {
        return this.userName;
    }

    public void setUserName(string userName)
    {
        this.userName = userName;
    }

    public int getPrimaryLevel()
    {
        return this.primaryLevel;
    }

    public void setPrimaryLevel(int primaryLevel)
    {
        this.primaryLevel = primaryLevel;
    }

    public List<Item> getInventory() {
		return this.inventory;
	}

	public void setInventory(List<Item> inventory) {
		this.inventory = inventory;
	}

	public Character getCharacter() {
		return this.character;
	}

	public void setCharacter(Character character) {
		this.character = character;
	}

	public int getPoints() {
		return this.points;
	}

	public void setPoints(int points) {
		this.points = points;
	}
    
}
