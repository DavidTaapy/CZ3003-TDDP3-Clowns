using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiModeTest
{
    [UnityTest]
    // Test the functionality of updating the user's points after the game ends.
    public IEnumerator game_end_update_user_points()
    {
        SceneManager.LoadScene("MultiMode");
        yield return new WaitForSeconds(3);
        
        var userPoints = GameObject.FindObjectOfType<ScoreKeeper>().CalculatePoints();

        Button skipQuestionButton = GameObject.Find("SkipQuestionButton").GetComponent<Button>();
        skipQuestionButton.onClick.Invoke();
        yield return new WaitForSeconds(105);

        var newUserPoints = GameObject.FindObjectOfType<ScoreKeeper>().CalculatePoints();
        Assert.AreNotEqual(userPoints, newUserPoints, "Failed on the following test: game_end_update_user_points!");
    }
}
