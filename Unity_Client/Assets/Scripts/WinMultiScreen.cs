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
        string playerName = PlayerPrefs.GetString("playerName");
        GameObject.Find("TextDetails").GetComponent<Text>().text = "Congratulations!\n" + playerName + ", you won the match with " + PlayerPrefs.GetString("winner") + "\n\n Your elo score is up by 100!";
        linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
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
