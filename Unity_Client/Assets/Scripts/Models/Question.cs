using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question 
{
    [SerializeField] string question;
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;
    [SerializeField] int primaryLevel;
    [SerializeField] string hint;

    public Question(string question, string[] answers, int correctAnswerIndex, int primaryLevel, string hint)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
        this.primaryLevel = primaryLevel;
        this.hint = hint;
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

    public string GetHint()
    {
        return hint;
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
