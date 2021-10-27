using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PastLeaderboard
{

    [SerializeField]
    int seasonID;

    [SerializeField]
    List<User> users;

    public PastLeaderboard(){

    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    public int getSeasonID()
    {
        return this.seasonID;
    }

    public void setSeasonID(int seasonID)
    {
        this.seasonID = seasonID;
    }

    public List<User> getUsers()
    {
        return this.users;
    }

    public void setUsers(List<User> users)
    {
        this.users = users;
    }


    


}