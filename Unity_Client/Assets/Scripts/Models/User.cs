using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class User
{
    public int eloRating;
    public long id;

    public string userName;

    public int primaryLevel;

    public User(int id, string username, int eloRating, int primaryLevel){
        this.id = id;
        this.userName = username;
        this.eloRating = eloRating;
        this.primaryLevel = primaryLevel;
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

    public long getId()
    {
        return this.id;
    }

    public void setId(long id)
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
    

    

    

// temporarily commented out for testing purposes
/*
    public List<Item> inventory;

    public int eloRating;

    

    public Character character;  
    */
}
