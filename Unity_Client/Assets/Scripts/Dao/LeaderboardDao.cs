using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;

public class LeaderboardDao : MonoBehaviour
{
    // http://localhost:3000/leaderboard
    
    HttpClient client = new HttpClient();

    public List<User> getLeaderboard(string url){
        
        HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        List<User> userRanking = JsonConvert.DeserializeObject<List<User>>(responseStr);
        return userRanking;
    }

    public PastLeaderboard getPastLeaderboard(string url, int seasonId){
        string urlWithParams = string.Format("{0}?seasonId={1}", url, seasonId);

        HttpResponseMessage response = client.GetAsync(urlWithParams).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        PastLeaderboard pastLeaderboard = JsonUtility.FromJson<PastLeaderboard>(responseStr);
        return pastLeaderboard;
    }
}