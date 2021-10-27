using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using Newtonsoft.Json;

public class rewardsManager : MonoBehaviour
{
    public GameObject firstPlaceChar;
    public GameObject secondPlaceChar;
    public GameObject thirdPlaceChar;
    public Text firstPlace;
    public Text secondPlace;
    public Text thirdPlace;
    HttpClient client = new HttpClient();
    // Start is called before the first frame update
    void Start()
    {
        //var linktoLeaderboard = GameObject.Find("LeaderboardDao").GetComponent<LeaderboardDao>();
        //List<User> userRanking = linktoLeaderboard.getLeaderboard("http://localhost:3000/leaderboard");
        //Debug.Log(userRanking.Count);
        //foreach (User u in userRanking)
        //{
        //        Debug.Log(u.ToJSON());
        //}
        //var sprite = Resources.Load<Sprite>("Sprites/Sprites (Final)/Character/Jelly 10");
        //firstPlaceChar.GetComponent<Image>().sprite = sprite;
        topThree();
    }

    public List<User> getLeaderboard(string url)
    {

        HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        List<User> userRanking = JsonConvert.DeserializeObject<List<User>>(responseStr);
        return userRanking;
    }

    public void topThree()
    {
        List<User> userRanking = getLeaderboard("http://localhost:3000/leaderboard");
        Debug.Log(userRanking[0]);
        for (int i = 0; i < Mathf.Min(3, userRanking.Count); i++)
        {
            // Debug.Log(userRanking[i].userName);
            if (userRanking[i].character.spriteSource != null)
            {
                if (i == 0)
                {
                    var sprite = Resources.Load<Sprite>(userRanking[i].character.spriteSource);
                    firstPlaceChar.GetComponent<Image>().sprite = sprite;
                    firstPlace.text = userRanking[i].userName;
                } else if (i == 1)
                {
                    var sprite = Resources.Load<Sprite>(userRanking[i].character.spriteSource);
                    secondPlaceChar.GetComponent<Image>().sprite = sprite;
                    secondPlace.text = userRanking[i].userName;
                } else if (i == 2)
                {
                    var sprite = Resources.Load<Sprite>(userRanking[i].character.spriteSource);
                    thirdPlaceChar.GetComponent<Image>().sprite = sprite;
                    thirdPlace.text = userRanking[i].userName;
                }
            }

        }

    }

    public class User
    {
        public string userName { get; set; }
        public int elorating { get; set; }
        public Character character { get; set; }
    }
    public class Character
    {
        public string characterDescription { get; set; }
        public string characterID { get; set; }
        public string characterName { get; set; }
        public string spriteSource { get; set; }
    }
}
