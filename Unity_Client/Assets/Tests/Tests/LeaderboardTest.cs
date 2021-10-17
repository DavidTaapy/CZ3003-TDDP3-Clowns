using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LeaderboardTest
{
    [UnityTest]
    // Comment: Check that order of users are arranged by highest score at first to lowest score at fifth
    public IEnumerator leaderboard_rows_check_arranged_by_descending_elo()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        RowUi rankOne = GameObject.Find("Row1").GetComponent<RowUi>();
        RowUi rankTwo = GameObject.Find("Row2").GetComponent<RowUi>();
        RowUi rankThree = GameObject.Find("Row3").GetComponent<RowUi>();
        RowUi rankFour = GameObject.Find("Row4").GetComponent<RowUi>();
        RowUi rankFive = GameObject.Find("Row5").GetComponent<RowUi>();

        int rankOneElo = int.Parse(rankOne.score.text);
        int rankTwoElo = int.Parse(rankTwo.score.text);
        int rankThreeElo = int.Parse(rankThree.score.text);
        int rankFourElo = int.Parse(rankFour.score.text);
        int rankFiveElo = int.Parse(rankFive.score.text);

        Assert.IsTrue(rankOneElo >= rankTwoElo); // Check rank 1 has higher elo than rank 2
        Assert.IsTrue(rankTwoElo >= rankThreeElo);  // Check rank 2 has higher elo than rank 3
        Assert.IsTrue(rankThreeElo >= rankFourElo); // Check rank 3 has higher elo than rank 4
        Assert.IsTrue(rankFourElo >= rankFiveElo);  // Check rank 4 has higher elo than rank 5
    }

    [UnityTest]
    // Comment: Check that view season rewards button can be clicked successfully
    public IEnumerator view_season_rewards_button_on_click_success()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Button seasonRewardButton = GameObject.Find("ViewSeasonRewardButton").GetComponent<Button>();
        seasonRewardButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(seasonRewardButton); // No warning or error on click
    }

    [UnityTest]
    // Comment: Check that there is a whatsapp share icon
    public IEnumerator whatsapp_logo_check_truthy()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Image whatsappLogo = GameObject.Find("WhatappsLogo").GetComponent<Image>();
        
        yield return new WaitForSeconds(3);
        Assert.IsTrue(whatsappLogo); // Check that it is truthy
    }

    [UnityTest]
    // Comment: Check that there is a telegram share icon
    public IEnumerator telegram_logo_check_truthy()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Image telegramLogo = GameObject.Find("TelegramLogo").GetComponent<Image>();
        
        yield return new WaitForSeconds(3);
        Assert.IsTrue(telegramLogo); // Check that it is truthy
    }

    [UnityTest]
    // Comment: Leaderboard check correct headers - my rank
    public IEnumerator leaderboard_header_my_rank_check_correct_text()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Text myRank = GameObject.Find("MyRankText").GetComponent<Text>();
        Assert.AreEqual(myRank.text, "My Rank");
    }

    [UnityTest]
    // Comment: Leaderboard check correct headers - my name
    public IEnumerator leaderboard_header_my_name_check_correct_text()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Text myRankName = GameObject.Find("MyRankName").GetComponent<Text>();
        Assert.AreEqual(myRankName.text, "My Name");
    }

    [UnityTest]
    // Comment: Leaderboard check correct headers - my elo
    public IEnumerator leaderboard_header_my_elo_check_correct_text()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Text myElo = GameObject.Find("MyElo").GetComponent<Text>();
        Assert.AreEqual(myElo.text, "Elo Score");
    }

    [UnityTest]
    // Comment: Leaderboard check that there are 5 rows
    public IEnumerator leaderboard_content_check_correct_five_rows()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        RowUi rankOne = GameObject.Find("Row1").GetComponent<RowUi>();
        RowUi rankTwo = GameObject.Find("Row2").GetComponent<RowUi>();
        RowUi rankThree = GameObject.Find("Row3").GetComponent<RowUi>();
        RowUi rankFour = GameObject.Find("Row4").GetComponent<RowUi>();
        RowUi rankFive = GameObject.Find("Row5").GetComponent<RowUi>();

        Assert.IsTrue(rankOne);
        Assert.IsTrue(rankTwo);
        Assert.IsTrue(rankThree);
        Assert.IsTrue(rankFour);
        Assert.IsTrue(rankFive);
    }

    [UnityTest]
    // Comment: Leaderboard check that all 5 names in rows are truthy
    public IEnumerator leaderboard_content_check_correct_all_rows_have_name()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Text rankOneName = GameObject.Find("Row1").GetComponent<RowUi>().name;
        Text rankTwoName = GameObject.Find("Row2").GetComponent<RowUi>().name;
        Text rankThreeName = GameObject.Find("Row3").GetComponent<RowUi>().name;
        Text rankFourName = GameObject.Find("Row4").GetComponent<RowUi>().name;
        Text rankFiveName = GameObject.Find("Row5").GetComponent<RowUi>().name;

        Assert.IsTrue(rankOneName);
        Assert.IsTrue(rankTwoName);
        Assert.IsTrue(rankThreeName);
        Assert.IsTrue(rankFourName);
        Assert.IsTrue(rankFiveName);
    }

    [UnityTest]
    // Comment: Leaderboard check that all 5 elos in rows are truthy
    public IEnumerator leaderboard_content_check_correct_all_rows_have_elo()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Text rankOneElo = GameObject.Find("Row1").GetComponent<RowUi>().score;
        Text rankTwoElo = GameObject.Find("Row2").GetComponent<RowUi>().score;
        Text rankThreeElo = GameObject.Find("Row3").GetComponent<RowUi>().score;
        Text rankFourElo = GameObject.Find("Row4").GetComponent<RowUi>().score;
        Text rankFiveElo = GameObject.Find("Row5").GetComponent<RowUi>().score;

        Assert.IsTrue(rankOneElo);
        Assert.IsTrue(rankTwoElo);
        Assert.IsTrue(rankThreeElo);
        Assert.IsTrue(rankFourElo);
        Assert.IsTrue(rankFiveElo);
    }

    [UnityTest]
    // Comment: Leaderboard check that all 5 ranks in rows are truthy
    public IEnumerator leaderboard_content_check_correct_all_rows_have_rank()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        Text rankOneRank = GameObject.Find("Row1").GetComponent<RowUi>().rank;
        Text rankTwoRank = GameObject.Find("Row2").GetComponent<RowUi>().rank;
        Text rankThreeRank = GameObject.Find("Row3").GetComponent<RowUi>().rank;
        Text rankFourRank = GameObject.Find("Row4").GetComponent<RowUi>().rank;
        Text rankFiveRank = GameObject.Find("Row5").GetComponent<RowUi>().rank;

        Assert.IsTrue(rankOneRank);
        Assert.IsTrue(rankTwoRank);
        Assert.IsTrue(rankThreeRank);
        Assert.IsTrue(rankFourRank);
        Assert.IsTrue(rankFiveRank);
    }
}
