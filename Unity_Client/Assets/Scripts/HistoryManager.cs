using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HistoryManager : MonoBehaviour
{
    //public UserQuestions userQuestions;
    public GameObject Content;
    public GameObject[] qnSlots;
    public User user;
    public List<Question> userQuestions;
    public string url_user;
    public int displayNum = 17;

    // Start is called before the first frame update
    void Start()
    {
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        var userId = "7HHcjbfJq1kD8VFMHHDq";
        user = linktoUserGet.getUser(url_user, userId);
        userQuestions = user.getCompletedQns();
        DisplayQuestions();
        Content.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DisplayQuestions()
    {
        for (int i = 0; i < userQuestions.Count; i++)
        {
            Debug.Log(userQuestions[i].ToJSON());
            qnSlots[i].gameObject.SetActive(true);
            int correctAns = userQuestions[i].GetCorrectAnswerIndex();


            qnSlots[i].transform.GetChild(0).GetComponent<Text>().text = userQuestions[i].GetQuestion();
            qnSlots[i].transform.GetChild(1).GetComponent<Text>().text = "Answer: " + userQuestions[i].GetAnswer(correctAns);
        }

      
        for (int i = userQuestions.Count; i < displayNum; i++)
        {
            string curr = "Questions (" + i.ToString() + ")";
            GameObject.Find(curr).SetActive(false);
        }

    }
}
