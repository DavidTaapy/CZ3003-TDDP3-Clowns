using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;

public class CharacterDao : MonoBehaviour
{
    // http://localhost:3000/allcharacter
    
    HttpClient client = new HttpClient();

    public List<Character> getAllCharacters(string url){
        
        //string urlWithParams = string.Format("{0}?name={1}", url, restaurantName);

        HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        Debug.Log(responseStr);
        List<Character> charList = JsonConvert.DeserializeObject<List<Character>>(responseStr);
        return charList;
    }

    public Character getCharacter(string url, string charID){
        string urlWithParams = string.Format("{0}?id={1}", url, charID);

        HttpResponseMessage response = client.GetAsync(urlWithParams).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        Character character = JsonUtility.FromJson<Character>(responseStr);
        Debug.Log(character.ToJSON());
        return character;
    }
}