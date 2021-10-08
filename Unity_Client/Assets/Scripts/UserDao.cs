using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserDao : MonoBehaviour
{
    // localhost:3000/user
    //public string url;

    public IEnumerator Get(string url)
    {
        using(UnityWebRequest www = UnityWebRequest.Get(url)){
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    //Debug.Log(result);

                    Debug.Log(result);
                    var user = JsonUtility.FromJson<User>(result);

                    Debug.Log("Username is " + user.username);
                    Debug.Log("primary level of user is  " + user.primaryLevel);
                    Debug.Log("User's points is currently at " + user.points);
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

        using(UnityWebRequest www = UnityWebRequest.Post(url, jsonData)){
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log(www.error);
            } else {
                if (www.isDone){
                    Debug.Log("reached inner loop");
                    /*
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);  
                    result = "{\"result\":" + result + "}"; 
                    var resultUserList = JsonHelper.FromJson<User>(result);

                    foreach (var item in resultUserList) {
                        Debug.Log(item.username);
                    }
                    */
                } else {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
}
