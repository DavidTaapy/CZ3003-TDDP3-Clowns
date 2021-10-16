using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleModeTest
{
    [UnityTest]
    // Test the functionality of skip question power-up. When the user clicks on
    // this button, the number of correct questions count increases by one.
    public IEnumerator skip_question_on_click_increase_correct_question()
    {
        SceneManager.LoadScene("SingleMode");
        yield return new WaitForSeconds(3);

        Button skipQuestionButton = GameObject.Find("SkipQuestionButton").GetComponent<Button>();
        skipQuestionButton.onClick.Invoke();

        var correctQuestions = GameObject.FindObjectOfType<ScoreKeeper>().GetCorrectAnswers();
        Assert.AreEqual(1, correctQuestions);

        yield return new WaitForSeconds(3);
    }

    [UnityTest]
    // Test the functionality of replay. When the user clicks on the replay
    // button, the points are reset to zero.
    public IEnumerator replay_on_click_reset_points()
    {
        SceneManager.LoadScene("SingleMode");
        yield return new WaitForSeconds(105);

        Button replayButton = GameObject.Find("ReplayButton").GetComponent<Button>();
        replayButton.onClick.Invoke();

        var points = GameObject.FindObjectOfType<ScoreKeeper>().CalculatePoints();
        Assert.AreEqual(0, points);

        yield return new WaitForSeconds(3);
    }

    [UnityTest]
    // Test the functionality of answer buttons. When the user clicks on the
    // answer button, the count of questions seen increases by one.
    public IEnumerator answer_on_click_increase_questions_seen()
    {
        SceneManager.LoadScene("SingleMode");
        yield return new WaitForSeconds(3);

        Button replayButton = GameObject.Find("AnswerButton (0)").GetComponent<Button>();
        replayButton.onClick.Invoke();

        yield return new WaitForSeconds(3);

        var newQuestionSeen = GameObject.FindObjectOfType<ScoreKeeper>().GetQuestionSeen();
        Assert.AreEqual(1, newQuestionSeen);

        yield return new WaitForSeconds(3);
    }
}
