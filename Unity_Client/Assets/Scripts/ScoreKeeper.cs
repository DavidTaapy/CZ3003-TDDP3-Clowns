using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    int pointPerCorrectAnswer = 5;

    List<Question> qnsGotCorrect = new List<Question>();
    List<Question> qnsGotWrong = new List<Question>();

    public int GetWrongAnswers(){
        return questionsSeen - correctAnswers;
    }

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
        if (questionsSeen != 0){
            return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
        } else {
            return 0;
        }
    }

    public int CalculatePoints()
    {
        return correctAnswers * pointPerCorrectAnswer;
    }

    public void SaveQuestionGotCorrect(Question currentQuestion)
    {
        qnsGotCorrect.Add(currentQuestion);
    }

    public void SaveQuestionGotWrong(Question currentQuestion)
    {
        qnsGotWrong.Add(currentQuestion);
    }

    public void resetFields(){
        this.correctAnswers = 0;
        this.questionsSeen = 0;
        this.qnsGotCorrect = new List<Question>();
        this.qnsGotWrong = new List<Question>();
    }
}
