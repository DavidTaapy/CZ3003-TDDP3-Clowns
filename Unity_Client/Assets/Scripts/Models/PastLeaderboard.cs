using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PastLeaderboard
{

    int seasonId;

    List<User> users;

    public PastLeaderboard(){

    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    public int getSeasonId()
    {
        return this.seasonId;
    }

    public void setSeasonId(int seasonId)
    {
        this.seasonId = seasonId;
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