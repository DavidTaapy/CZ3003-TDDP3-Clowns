using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using Newtonsoft.Json;

public class ScoreUi : MonoBehaviour
{
    public RowUi rowUi;
    // public ScoreManager scoreManager;

    LeaderboardDao linkToLeaderboard;

    List<User> userRanking;

    string url_leaderboard = "http://localhost:3000/leaderboard";

    void Start()
    {
        linkToLeaderboard = GameObject.Find("LeaderboardDao").GetComponent<LeaderboardDao>();
        userRanking = linkToLeaderboard.getLeaderboard(url_leaderboard);
        loadLeaderboard();
    }

    public void loadLeaderboard()
    {
        
        for (int i = 0; i < Mathf.Min(5, userRanking.Count); i++)
        {
            // Debug.Log(userRanking[i].userName);
            if (userRanking[i] != null)
            {
                var row = Instantiate(rowUi, transform).GetComponent<RowUi>();
                row.gameObject.name = "Row" + (i + 1).ToString();
                row.rank.text = (i + 1).ToString();
                row.name.text = userRanking[i].getUserName();
                row.score.text = userRanking[i].getEloRating().ToString();
            }
        }
    }
}
