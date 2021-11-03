using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Profiling;
using System;
using TMPro;

public class PerformanceTest
{
    // Testing for Login, need to change to a verified email
    [UnityTest, Performance]
    public IEnumerator Login()
    {
        using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
        {
            SceneManager.LoadScene("LoginFirebase");
        }
        yield return null;

        yield return new WaitForSeconds(.001f);

        TMP_InputField Email = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        Email.text = "guow0017@e.ntu.edu.sg";


        TMP_InputField Password = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        Password.text = "password";

        yield return new WaitForSeconds(.001f);

        Button LoginButton = GameObject.Find("Login_Btn").GetComponent<Button>();
        LoginButton.onClick.Invoke();

        yield return Measure.Frames().Run();
    }

    // Custom measurement to capture total allocated and reserved memory
    [Test, Performance, Version("1")]
    public void Measure_Empty()
    {
        var allocated = new SampleGroup("TotalAllocatedMemory", SampleUnit.Megabyte);
        var reserved = new SampleGroup("TotalReservedMemory", SampleUnit.Megabyte);
        Measure.Custom(allocated, Profiler.GetTotalAllocatedMemoryLong() / 1048576f);
        Measure.Custom(reserved, Profiler.GetTotalReservedMemoryLong() / 1048576f);
    }

    // Scene Measurement
    [UnityTest, Performance]
    public IEnumerator Rendering_Scene()
    {
        using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
        {
            SceneManager.LoadScene("MainMenu");
        }
        yield return null;

        yield return Measure.Frames().Run();
    }

    // From MainMenu to SingleMode
    [UnityTest, Performance]
    public IEnumerator MainMenu_to_SingleMode()
    {
        using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
        {
            SceneManager.LoadScene("MainMenu");
        }
        yield return null;

        Button singlePlayerButton = GameObject.Find("Single Player Button").GetComponent<Button>();
        singlePlayerButton.onClick.Invoke();

        yield return new WaitForSeconds(.001f);

        Button skipQuestionButton = GameObject.Find("SkipQuestionButton").GetComponent<Button>();
        skipQuestionButton.onClick.Invoke();

        yield return new WaitForSeconds(.001f);

        Button showHintButton = GameObject.Find("ShowHintButton").GetComponent<Button>();
        showHintButton.onClick.Invoke();

        yield return Measure.Frames().Run();
    }

    // From MainMenu to MP_Finding (matching player)
    [UnityTest, Performance]
    public IEnumerator MainMenu_to_MP_Finding()
    {
        using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
        {
            SceneManager.LoadScene("MainMenu");
        }
        yield return null;

        Button singlePlayerButton = GameObject.Find("Multi Player Button").GetComponent<Button>();
        singlePlayerButton.onClick.Invoke();

        yield return new WaitForSeconds(.001f);

        Button startButton = GameObject.Find("Start Button").GetComponent<Button>();
        startButton.onClick.Invoke();

        yield return Measure.Frames().Run();
    }

    // Click answer buttons in single player mode
    [UnityTest, Performance]
    public IEnumerator Click_Answer_Button()
    {
        using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
        {
            SceneManager.LoadScene("SingleMode");
        }
        yield return null;

        Button answerButton0 = GameObject.Find("AnswerButton (0)").GetComponent<Button>();
        answerButton0.onClick.Invoke();

        yield return new WaitForSeconds(.001f);

        Button answerButton1 = GameObject.Find("AnswerButton (1)").GetComponent<Button>();
        answerButton1.onClick.Invoke();

        yield return new WaitForSeconds(.001f);

        Button answerButton2 = GameObject.Find("AnswerButton (2)").GetComponent<Button>();
        answerButton2.onClick.Invoke();

        yield return new WaitForSeconds(0.1f);

        Button answerButton3 = GameObject.Find("AnswerButton (3)").GetComponent<Button>();
        answerButton3.onClick.Invoke();

        yield return Measure.Frames().Run();
    }
}
