using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;

public class QuestionDao : MonoBehaviour
{
    // http://localhost:3000/questions
    
    HttpClient client = new HttpClient();

    public List<QuestionSO> getQuestions(string url, int primaryLevel){
        string urlWithParams = string.Format("{0}?lvl={1}", url, primaryLevel);
        Debug.Log(urlWithParams);
        HttpResponseMessage response = client.GetAsync(urlWithParams).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        List<QuestionSO> qnList = JsonConvert.DeserializeObject<List<QuestionSO>>(responseStr);
        Debug.Log(qnList[0]);
        return qnList;
    }
}