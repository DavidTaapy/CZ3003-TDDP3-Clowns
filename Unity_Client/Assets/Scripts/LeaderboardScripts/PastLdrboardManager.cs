using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using Newtonsoft.Json;

public class PastLdrboardManager : MonoBehaviour
{
    public GameObject firstPlaceUser;
    public GameObject secondPlaceUser;
    public GameObject thirdPlaceUser;

    public Text seasonText;

    public string url_pastLdrboard = "http://localhost:3000/pastleaderboard";
    LeaderboardDao linkToleaderboard;
    PastLeaderboard pastLeaderboard;

    Sprite sprite;

    int seasonId = 2;

    void Awake()
    {
        linkToleaderboard = GameObject.Find("LeaderboardDao").GetComponent<LeaderboardDao>();
        pastLeaderboard = linkToleaderboard.getPastLeaderboard(url_pastLdrboard, seasonId);
        displayRankings();

        seasonText.text = string.Format("Season {0} Winners", seasonId);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void displayRankings(){
        var users = pastLeaderboard.getUsers();

        sprite = Resources.Load<Sprite>(users[0].getCharacter().getSpriteSource());
        firstPlaceUser.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        firstPlaceUser.transform.GetChild(1).GetComponent<Text>().text = users[0].getUserName();

        sprite = Resources.Load<Sprite>(users[1].getCharacter().getSpriteSource());
        secondPlaceUser.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        secondPlaceUser.transform.GetChild(1).GetComponent<Text>().text = users[1].getUserName();

        sprite = Resources.Load<Sprite>(users[2].getCharacter().getSpriteSource());
        thirdPlaceUser.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        thirdPlaceUser.transform.GetChild(1).GetComponent<Text>().text = users[2].getUserName();
    }
}
