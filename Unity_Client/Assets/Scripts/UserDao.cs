using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class UserDao : MonoBehaviour
{
    // localhost:3000/user

    public IEnumerator Get(string url, int userId)
    {
        string urlWithParams = string.Format("{0}?id={1}", url, userId);
        using(UnityWebRequest webRequest = UnityWebRequest.Get(urlWithParams)){
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.isDone)
                {
                    // handle the result
                    string result = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    try {
                        User user = JsonConvert.DeserializeObject <User> (result);

                        Debug.Log("Username is " + user.userName);
                        Debug.Log("primary level of user is  " + user.primaryLevel);
                        Debug.Log("User's elo points is currently at " + user.eloRating);
                    } catch (Exception e){
                        Debug.Log("User id does not exisit! Please check.\n" + e); 
                    }
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }   
        }
    }

    public IEnumerator Post(string url, User user){
        var jsonData = JsonUtility.ToJson(user);
        Debug.Log(jsonData);

        using(UnityWebRequest webRequest = UnityWebRequest.Post(url, jsonData)){
            webRequest.SetRequestHeader("content-type", "application/json");
            webRequest.uploadHandler.contentType = "application/json";
            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log(webRequest.error);
            } else {
                if (webRequest.isDone){
                    // handle the result
                    string result = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    Debug.Log(result);
                } else {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

    public IEnumerator Delete(string url, int userId)
    {
        string urlWithParams = string.Format("{0}?id={1}", url, userId);
        Debug.Log("url now: " + urlWithParams);
        using(UnityWebRequest webRequest = UnityWebRequest.Delete(urlWithParams)){           
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.isDone)
                {
                    // handle the result
                    string result = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }   
        }
    }

    public IEnumerator Put(string url, User user){
        var jsonData = JsonUtility.ToJson(user);
        Debug.Log(jsonData);
        
        using(UnityWebRequest webRequest = UnityWebRequest.Put(url, jsonData)){
            webRequest.SetRequestHeader("content-type", "application/json");
            webRequest.uploadHandler.contentType = "application/json";
            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.isDone)
                {
                    string result = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    Debug.Log(result);
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }   
        }
    }
}
