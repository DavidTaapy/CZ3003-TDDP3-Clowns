using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RegisterTest
{
    [UnityTest]
    // Comment: Check for the presence of the username, email, password and level selection fields
    public IEnumerator all_ui_components_check_truthy()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField usernameInput = GameObject.Find("Username_Input").GetComponent<TMP_InputField>();
        TMP_InputField emailInput = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        TMP_InputField passwordInput = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        TMP_Dropdown levelInput = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();
        Assert.AreEqual(usernameInput.GetType(), typeof(TMP_InputField)); 
        Assert.AreEqual(emailInput.GetType(), typeof(TMP_InputField));
        Assert.AreEqual(passwordInput.GetType(), typeof(TMP_InputField));
        Assert.AreEqual(levelInput.GetType(), typeof(TMP_Dropdown));
    }

    [UnityTest]
    // Comment: Check that username field stores input correctly
    public IEnumerator username_input_on_text_entered_changed()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField usernameInput = GameObject.Find("Username_Input").GetComponent<TMP_InputField>();

        usernameInput.text = "testUsername";
        Assert.AreEqual(usernameInput.text, "testUsername");
    }

    [UnityTest]
    // Comment: Check that email field stores input correctly
    public IEnumerator email_input_on_text_entered_changed()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField emailInput = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();

        emailInput.text = "test@test.com";
        Assert.AreEqual(emailInput.text, "test@test.com");
    }

    [UnityTest]
    // Comment: Check that password field stores input correctly
    public IEnumerator password_input_on_text_entered_changed()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField passwordInput = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();

        passwordInput.text = "password";
        Assert.AreEqual(passwordInput.text, "password");
    }

    [UnityTest]
    // Comment: Check that no input register fails and asks user for username
    public IEnumerator no_input_on_register_return_warning()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        Button registerButton = GameObject.Find("Register_Button").GetComponent<Button>();
        registerButton.onClick.Invoke();

        yield return new WaitForSeconds(3);

        TMP_Text warningMessage = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual(warningMessage.text, "Missing Username");
    }

    [UnityTest]
    // Comment: Check that no username register fails and asks user for username
    public IEnumerator no_username_on_register_return_warning()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        // Adding email and password without username
        TMP_InputField emailInput = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        TMP_InputField passwordInput = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        emailInput.text = "test@test.com";
        passwordInput.text = "password";

        Button registerButton = GameObject.Find("Register_Button").GetComponent<Button>();
        registerButton.onClick.Invoke();

        yield return new WaitForSeconds(3);

        TMP_Text warningMessage = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual(warningMessage.text, "Missing Username");
    }

    [UnityTest]
    // Comment: Check that no password register fails and asks user for username
    public IEnumerator no_password_on_register_return_warning()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        // Setting username and email without password
        TMP_InputField usernameInput = GameObject.Find("Username_Input").GetComponent<TMP_InputField>();
        TMP_InputField emailInput = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        usernameInput.text = "testUsername";
        emailInput.text = "test@test.com";
        
        Button registerButton = GameObject.Find("Register_Button").GetComponent<Button>();
        registerButton.onClick.Invoke();

        yield return new WaitForSeconds(3);

        TMP_Text warningMessage = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual(warningMessage.text, "Missing Password");
    }

    [UnityTest]
    // Comment: Check that no email register fails and asks user for username
    public IEnumerator no_email_on_register_return_warning()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField usernameInput = GameObject.Find("Username_Input").GetComponent<TMP_InputField>();
        TMP_InputField passwordInput = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        usernameInput.text = "testUsername";
        passwordInput.text = "password";

        Button registerButton = GameObject.Find("Register_Button").GetComponent<Button>();
        registerButton.onClick.Invoke();

        yield return new WaitForSeconds(3);

        TMP_Text warningMessage = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual(warningMessage.text, "Missing Email");
    }

    [UnityTest]
    // Comment: Check that if all three information are given validly, registration is successful
    public IEnumerator correct_information_register_success()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField usernameInput = GameObject.Find("Username_Input").GetComponent<TMP_InputField>();
        TMP_InputField passwordInput = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        TMP_InputField emailInput = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        usernameInput.text = "testUsername12345";
        passwordInput.text = "password";
        emailInput.text = "newtest12345@test.com";

        Button registerButton = GameObject.Find("Register_Button").GetComponent<Button>();
        registerButton.onClick.Invoke();

        yield return new WaitForSeconds(3);

        TMP_Text warningMessage = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual(warningMessage.text, ""); // No warning message
    }

    [UnityTest]
    // Comment: Check that duplicate emails returns a warning
    public IEnumerator duplicate_email_register_returns_warning()
    {
        SceneManager.LoadScene("RegisterFirebase");
        yield return new WaitForSeconds(3);

        TMP_InputField usernameInput = GameObject.Find("Username_Input").GetComponent<TMP_InputField>();
        TMP_InputField passwordInput = GameObject.Find("Password_Input").GetComponent<TMP_InputField>();
        TMP_InputField emailInput = GameObject.Find("Email_Input").GetComponent<TMP_InputField>();
        usernameInput.text = "testUsername12345";
        passwordInput.text = "password";
        emailInput.text = "newtest12345@test.com";

        Button registerButton = GameObject.Find("Register_Button").GetComponent<Button>();
        registerButton.onClick.Invoke();

        yield return new WaitForSeconds(3);

        TMP_Text warningMessage = GameObject.Find("Warning_Text").GetComponent<TMP_Text>();
        Assert.AreEqual(warningMessage.text, "Email Already In Use"); // No warning message
    }
}
