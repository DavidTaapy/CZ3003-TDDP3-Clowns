using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations!\nYou got a score of " + 
                                scoreKeeper.CalculateScore() + "%!\n You have answered "
                                + scoreKeeper.GetCorrectAnswers() + " out of " +
                                scoreKeeper.GetQuestionSeen() + " correctly!";
    }
}
