using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class AccountManager : MonoBehaviour
{
    // Variables
    private string userEmail;
    private string userPassword;
    private string username;
    public GameObject loginPanel;

    public void Start()
    {
        // In case PlayFab is not logged in
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "FAEA2";
        };

        // For testing using custom ID
        // var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        // PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        // Delete all saved data
        // PlayerPrefs.DeleteAll();

        // Checking if there was any previous logins
        if(PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            // Login with player prefs email and password
            var request = new LoginWithEmailAddressRequest
            {
                Email = userEmail,
                Password = userPassword
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        };
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your second successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
    }

    // If login fails -> Redirect to registration
    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = userEmail,
            Password = userPassword,
            Username = username
        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }

    public void GetUsername(string usernameIn)
    {
        username = usernameIn;
    }

    public void onClickLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = userEmail,
            Password = userPassword
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
}