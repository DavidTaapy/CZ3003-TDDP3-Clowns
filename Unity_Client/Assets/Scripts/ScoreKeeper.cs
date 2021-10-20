using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    int pointPerCorrectAnswer = 5;
    List<QuestionSO> questionsGotCorrect = new List<QuestionSO>();
    List<QuestionSO> questionsGotWrong = new List<QuestionSO>();

    List<Question> qnsGotCorrect = new List<Question>();
    List<Question> qnsGotWrong = new List<Question>();

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }

    public int CalculatePoints()
    {
        return correctAnswers * pointPerCorrectAnswer;
    }

    public void SaveQuestionGotCorrect(QuestionSO currentQuestion)
    {
        questionsGotCorrect.Add(currentQuestion);
    }

    public void SaveQuestionGotWrong(QuestionSO currentQuestion)
    {
        questionsGotWrong.Add(currentQuestion);
    }

    // Duplicated methods here
    public void SaveQuestionGotCorrect(Question currentQuestion)
    {
        qnsGotCorrect.Add(currentQuestion);
    }

    public void SaveQuestionGotWrong(Question currentQuestion)
    {
        qnsGotWrong.Add(currentQuestion);
    }

    public string[] GetQuestionsGotCorrect()
    {
        string[] correctQuestions = new string[] { };
        int i = 0;

        foreach (var question in questionsGotCorrect)
        {
            correctQuestions[i] = question.GetQuestion();
            i += 1;
        }

        return correctQuestions;
    }

    public string[] GetQuestionsGotWrong()
    {
        string[] wrongQuestions = new string[] { };
        int i = 0;

        foreach (var question in questionsGotCorrect)
        {
            wrongQuestions[i] = question.GetQuestion();
            i += 1;
        }

        return wrongQuestions;
    }
}
