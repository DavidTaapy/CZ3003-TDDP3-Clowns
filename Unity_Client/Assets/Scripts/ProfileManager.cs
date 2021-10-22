using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProfileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string url_user;

    void Start()
    {
        var userId = "testttt";
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        User user = linktoUserGet.getUser(url_user, userId);

        Display(user);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Display(User user)
    {
        int total_qns = user.getCorrectQns() + user.getWrongQns();
        int correct_percent = (user.getCorrectQns() * 100 )/ total_qns;
   
        GameObject.Find("Image").GetComponentInChildren<Text>().text = user.getEloRating().ToString();
        GameObject.Find("Image (1)").GetComponentInChildren<Text>().text = total_qns.ToString();
        GameObject.Find("Image (2)").GetComponentInChildren<Text>().text = correct_percent.ToString() + "%";
    }
}
