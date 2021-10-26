using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;

public class CharacterDao : MonoBehaviour
{
    // http://localhost:3000/character
    
    HttpClient client = new HttpClient();

    public List<Character> getAllCharacters(string url){
        
        //string urlWithParams = string.Format("{0}?name={1}", url, restaurantName);

        HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        Debug.Log(responseStr);
        List<Character> charList = JsonConvert.DeserializeObject<List<Character>>(responseStr);
        return charList;
    }
}