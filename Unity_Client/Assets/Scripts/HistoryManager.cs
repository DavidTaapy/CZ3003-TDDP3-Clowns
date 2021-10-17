using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HistoryManager : MonoBehaviour
{
    //public UserQuestions userQuestions;
    public GameObject QnPanel;
    public GameObject[] questionSlots;
    public List<Question> userQuestions;
    public string url_qn;

    // Start is called before the first frame update
    void Start()
    {
        var linktoQuestionGet = GameObject.Find("QuestionDao").GetComponent<QuestionDao>();
        var primaryLevel = 1;
        userQuestions = linktoQuestionGet.getQuestions(url_qn, primaryLevel);
        DisplayQuestions();
        QnPanel.gameObject.SetActive(true);
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

            questionSlots[i].gameObject.SetActive(true);
            int correctAns = userQuestions[i].GetCorrectAnswerIndex();
            

            questionSlots[i].transform.GetChild(0).GetComponent<Text>().text = userQuestions[i].GetQuestion();
            questionSlots[i].transform.GetChild(1).GetComponent<Text>().text = "Answer: " + userQuestions[i].GetAnswer(correctAns);
        }
    }
}
