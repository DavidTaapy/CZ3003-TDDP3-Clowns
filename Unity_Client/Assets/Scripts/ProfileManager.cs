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
        string userId = PlayerPrefs.GetString("uid");
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
        GameObject.Find("Welcome").GetComponent<UnityEngine.UI.Text>().text = "Welcome " + user.getUserName() + "!";
        Text eloText = GameObject.Find("Image").GetComponentInChildren<Text>();
        eloText.text = user.getEloRating().ToString();
        eloText.fontStyle = FontStyle.Bold;
        eloText.fontSize = 16   ;
        eloText.alignment = TextAnchor.MiddleCenter;
        Text qnsText = GameObject.Find("Image (1)").GetComponentInChildren<Text>();
        qnsText.text = total_qns.ToString();
        qnsText.fontStyle = FontStyle.Bold;
        qnsText.fontSize = 16;
        qnsText.alignment = TextAnchor.MiddleCenter;
        Text percentText = GameObject.Find("Image (2)").GetComponentInChildren<Text>();
        percentText.text = correct_percent.ToString() + "%";
        percentText.fontStyle = FontStyle.Bold;
        percentText.fontSize = 16;
        percentText.alignment = TextAnchor.MiddleCenter;
    }
}
