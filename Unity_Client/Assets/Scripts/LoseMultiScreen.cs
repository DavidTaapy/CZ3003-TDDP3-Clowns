using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoseMultiScreen : MonoBehaviour
{

    string url_user = "http://localhost:3000/user";
    UserDao linktoUserGet;
    User currentUser;
    void Awake()
    {
        string playerName = PlayerPrefs.GetString("playerName");
        string opponentName = linktoUserGet.getUser(url_user, PlayerPrefs.GetString("winner")).getUserName();
        GameObject.Find("TextDetails").GetComponent<Text>().text = "Try better next time!\n" + playerName + ", you lost the match to." + opponentName + "\n\n Your elo score is down by 50!";
        linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        currentUser = linktoUserGet.getUser(url_user, PlayerPrefs.GetString("uid"));
        UpdateScore();
    }


    private void UpdateScore(){
        int eloRating = currentUser.getEloRating();
        eloRating -= 50;
        currentUser.setEloRating(eloRating);
        linktoUserGet.updateUser(url_user, currentUser);
    }
}
