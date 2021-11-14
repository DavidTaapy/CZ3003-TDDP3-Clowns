using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WinMultiScreen : MonoBehaviour
{

    string url_user = "http://localhost:3000/user";
    UserDao linktoUserGet;
    User currentUser;
    void Awake()
    {
        linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        string playerName = PlayerPrefs.GetString("playerName");
        string opponentName = linktoUserGet.getUser(url_user, PlayerPrefs.GetString("loser")).getUserName();
        GameObject.Find("TextDetails").GetComponent<Text>().text = "Congratulations!\n" + playerName + ", you won the match with " + opponentName + "\n\n Your elo score is up by 100!";
        currentUser = linktoUserGet.getUser(url_user, PlayerPrefs.GetString("uid"));
        UpdateScore();
    }

    private void UpdateScore(){
        int eloRating = currentUser.getEloRating();
        eloRating += 100;
        currentUser.setEloRating(eloRating);
        linktoUserGet.updateUser(url_user, currentUser);
    }
}