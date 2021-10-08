using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using apiManagers;

public class ClientApi : MonoBehaviour
{
    // Start is called before the first frame update
    public string url;
    public DataAcessManager dataAcessManager;
    void Start()
    {
        StartCoroutine(Get(url));
    }

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

                        //enemyViewController.DisplayEnemyData(enemy.name, enemy.health.ToString(), enemy.attack.ToString());

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
}
