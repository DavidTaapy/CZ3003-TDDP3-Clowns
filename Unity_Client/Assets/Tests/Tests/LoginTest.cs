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
    // Checks that valid inputs does result in successful login
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

    // Checks that missing email results in warning text prompting user for the email
    [UnityTest]
    public IEnumerator MissingEmail_Input_LoginButton_Onclick_Warning_Text()
    {
        SceneManager.LoadScene("LoginFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField Email_Input = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        Email_Input.text = "";
        Email_Input.textComponent.SetText("");
        TMP_InputField Password_Input = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        Password_Input.text = "123456";
        Password_Input.textComponent.SetText("123456");

        Button LoginButton = GameObject.Find("Login_Btn").GetComponent<Button>();
        LoginButton.onClick.Invoke();
        yield return new WaitForSeconds(1);
        //TMP_Text Feedback_Text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        //TextMeshProUGUI feedback_text = GameObject.Find("Confirm_Text").GetComponent<TextMeshProUGUI>();
        TMP_Text feedback_text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual("Missing Email", feedback_text.text);

        yield return new WaitForSeconds(3);
    }

    // Checks that missing password results in warning text prompting user for the password
    [UnityTest]
    public IEnumerator MissingPassword_LoginButton_Onclick_Warning_Text()
    {
        SceneManager.LoadScene("LoginFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField Email_Input = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        Email_Input.text = "test@test.com";
        Email_Input.textComponent.SetText("test@test.com");
        TMP_InputField Password_Input = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        Password_Input.text = "";
        Password_Input.textComponent.SetText("");

        Button LoginButton = GameObject.Find("Login_Btn").GetComponent<Button>();
        LoginButton.onClick.Invoke();
        yield return new WaitForSeconds(1);
        //TMP_Text Feedback_Text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        //TextMeshProUGUI feedback_text = GameObject.Find("Confirm_Text").GetComponent<TextMeshProUGUI>();
        TMP_Text feedback_text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual("Missing Password", feedback_text.text);

        yield return new WaitForSeconds(3);
    }

    // Check that wrong password results in login failure and warning text informing user of the wrong password
    [UnityTest]
    public IEnumerator WrongPassword_LoginButton_Onclick_Warning_Text()
    {
        SceneManager.LoadScene("LoginFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField Email_Input = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        Email_Input.text = "test@test.com";
        Email_Input.textComponent.SetText("test@test.com");
        TMP_InputField Password_Input = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        Password_Input.text = "sdfasdf";
        Password_Input.textComponent.SetText("sdfasdf");

        Button LoginButton = GameObject.Find("Login_Btn").GetComponent<Button>();
        LoginButton.onClick.Invoke();
        yield return new WaitForSeconds(1);
        //TMP_Text Feedback_Text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        //TextMeshProUGUI feedback_text = GameObject.Find("Confirm_Text").GetComponent<TextMeshProUGUI>();
        TMP_Text feedback_text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual("Wrong Password", feedback_text.text);

        yield return new WaitForSeconds(3);
    }

    // Check that wrong email results in login failure and warning text informing user of the wrong email
    [UnityTest]
    public IEnumerator InvalidEmail_LoginButton_Onclick_Warning_Text()
    {
        SceneManager.LoadScene("LoginFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField Email_Input = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        Email_Input.text = "safdlkj";
        Email_Input.textComponent.SetText("safdlkj.com");
        TMP_InputField Password_Input = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        Password_Input.text = "sdfasdf";
        Password_Input.textComponent.SetText("sdfasdf");

        Button LoginButton = GameObject.Find("Login_Btn").GetComponent<Button>();
        LoginButton.onClick.Invoke();
        yield return new WaitForSeconds(1);
        //TMP_Text Feedback_Text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        //TextMeshProUGUI feedback_text = GameObject.Find("Confirm_Text").GetComponent<TextMeshProUGUI>();
        TMP_Text feedback_text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual("Invalid Email", feedback_text.text);

        yield return new WaitForSeconds(3);
    }

    // Check that wrong username results in login failure and warning text informing user of the wrong username
    [UnityTest]
    public IEnumerator UserNotFound_LoginButton_Onclick_Warning_Text()
    {
        SceneManager.LoadScene("LoginFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField Email_Input = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        Email_Input.text = "notanaccount@test.com";
        Email_Input.textComponent.SetText("notanaccount@test.com");
        TMP_InputField Password_Input = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        Password_Input.text = "sdfasdf";
        Password_Input.textComponent.SetText("sdfasdf");

        Button LoginButton = GameObject.Find("Login_Btn").GetComponent<Button>();
        LoginButton.onClick.Invoke();
        yield return new WaitForSeconds(1);
        //TMP_Text Feedback_Text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        //TextMeshProUGUI feedback_text = GameObject.Find("Confirm_Text").GetComponent<TextMeshProUGUI>();
        TMP_Text feedback_text = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual("Account does not exist", feedback_text.text);

        yield return new WaitForSeconds(3);
    }
}
