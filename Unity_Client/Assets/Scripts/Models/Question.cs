using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question 
{
    [SerializeField] string question;
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;
    //[SerializeField] int primaryLevel;

    public Question(string question, string[] answers, int correctAnswerIndex)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }



    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
