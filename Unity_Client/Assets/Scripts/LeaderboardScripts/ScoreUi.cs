using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Net.Http;
using Newtonsoft.Json;

public class ScoreUi : MonoBehaviour
{
    public RowUi rowUi;
    // public ScoreManager scoreManager;

    HttpClient client = new HttpClient();

    void Start()
    {
        //var scores = scoreManager.GetHighScores().ToArray();
        //for (int i = 0; i < scores.Length; i++)
        //{
        //    var row = Instantiate(rowUi, transform).GetComponent<RowUi>();
        //    row.rank.text = (i + 1).ToString();
        //    row.name.text = scores[i].name;
        //    row.score.text = scores[i].score.ToString();
        //}
        loadLeaderboard();
    }

    public List<User> getLeaderboard(string url)
    {

        HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        List<User> userRanking = JsonConvert.DeserializeObject<List<User>>(responseStr);
        return userRanking;
    }

    public void loadLeaderboard()
    {
        List<User> userRanking = getLeaderboard("http://localhost:3000/leaderboard");
        // Debug.Log(userRanking);
        for (int i = 0; i < 5; i++)
        {
            // Debug.Log(userRanking[i].userName);
            var row = Instantiate(rowUi, transform).GetComponent<RowUi>();
            row.rank.text = (i + 1).ToString();
            row.name.text = userRanking[i].userName;
            row.score.text = userRanking[i].elorating.ToString();
        }
        
    }

    public class User
    {
        public string userName { get; set; }
        public int elorating { get; set; }
    }
}
