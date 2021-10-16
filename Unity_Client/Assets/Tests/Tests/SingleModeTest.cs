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
    public IEnumerator SkipQuestionIncreaseCorrectQuestion()
    {
        SceneManager.LoadScene("SingleMode");
        // Use yield to skip a frame.
        yield return new WaitForSeconds(3);

        Button skipQuestionButton = GameObject.Find("SkipQuestionButton").GetComponent<Button>();
        skipQuestionButton.onClick.Invoke();

        var correctQuestions = GameObject.FindObjectOfType<ScoreKeeper>().GetCorrectAnswers();
        Assert.AreEqual(1, correctQuestions);

        yield return new WaitForSeconds(3);
    }

    [UnityTest]
    public IEnumerator ReplayResetQuestionSeen()
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
    public IEnumerator SelectAnswerIncreaseQuestionSeen()
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
