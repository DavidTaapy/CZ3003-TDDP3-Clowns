using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Net.Http;

public class UserDao : MonoBehaviour
{
    // http://localhost:3000/user
    
    HttpClient client = new HttpClient();

    public User getUser(string url, string userId){
        string urlWithParams = string.Format("{0}?id={1}", url, userId);

        HttpResponseMessage response = client.GetAsync(urlWithParams).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        User user = JsonUtility.FromJson<User>(responseStr);
        //User user = JsonConvert.DeserializeObject<User>(responseStr);
        Debug.Log(user.ToJSON());
        return user;
    }

    public string createUser(string url, User newUser) {
        var formContent = new StringContent(newUser.ToJSON(), Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PostAsync(url, formContent).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return responseStr;
    }

    public string updateUser(string url, User user){
        var formContent = new StringContent(user.ToJSON(), Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PutAsync(url, formContent).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return responseStr;
    }

    public string deleteUser(string url, string userId){
        string urlWithParams = string.Format("{0}?id={1}", url, userId);

        HttpResponseMessage response = client.DeleteAsync(urlWithParams).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        
        return responseStr;
    }
}
