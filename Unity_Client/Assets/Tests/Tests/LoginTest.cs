using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoginTest
{
    [UnityTest]
    public IEnumerator Valid_Input_LoginButton_Onclick_Warning_Text()
    {
        SceneManager.LoadScene("LoginFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField Email_Input = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        Email_Input.text = "test@test.com";
        Email_Input.textComponent.SetText("test@test.com");
        TMP_InputField Password_Input = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        Password_Input.text = "123456";
        Password_Input.textComponent.SetText("123456");

        Button LoginButton = GameObject.Find("Login_Btn").GetComponent<Button>();
        LoginButton.onClick.Invoke();
        yield return new WaitForSeconds(1);
        //TMP_Text Feedback_Text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        //TextMeshProUGUI feedback_text = GameObject.Find("Confirm_Text").GetComponent<TextMeshProUGUI>();
        TMP_Text feedback_text = GameObject.Find("Confirm_Text").GetComponent<TMP_Text>();
        Assert.AreEqual("Logged In", feedback_text.text);

        yield return new WaitForSeconds(3);
    }
}
