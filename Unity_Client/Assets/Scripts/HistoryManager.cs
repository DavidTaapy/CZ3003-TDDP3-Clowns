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
    public List<Question> userQuestions;
    public string url_qn;
    //public int displayNum = 5;

    // Start is called before the first frame update
    void Start()
    {
        var linktoQuestionGet = GameObject.Find("QuestionDao").GetComponent<QuestionDao>();
        var primaryLevel = 2;
        userQuestions = linktoQuestionGet.getQuestions(url_qn, primaryLevel);
        userQuestions = linktoQuestionGet.getQuestions(url_qn, primaryLevel);
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
            qnSlots[i].gameObject.SetActive(true);
            int correctAns = userQuestions[i].GetCorrectAnswerIndex();


            qnSlots[i].transform.GetChild(0).GetComponent<Text>().text = userQuestions[i].GetQuestion();
            qnSlots[i].transform.GetChild(1).GetComponent<Text>().text = "Answer: " + userQuestions[i].GetAnswer(correctAns);
        }

      
        /*for (int i = userQuestions.Count; i < displayNum; i++)
        {
            string curr = "questionslots (" + i.ToString() + ")";
            Debug.Log(curr);
            GameObject.Find(curr).SetActive(false);
        }*/

    }
}
