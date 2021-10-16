using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Linq;

public class RegisterManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    //public TMP_InputField passwordRegisterVerifyField;
    public int grade;
    public TMP_Text warningRegisterText;
    public string url_user;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
                auth = FirebaseAuth.DefaultInstance;
                DBreference = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    public void ClearRegisterFeilds()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
    }

    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text, grade));
    }

    public void HandleDropdownData(int val)
    {
        switch (val)
        {
            case 0:
                grade = 1;
                break;
            case 1:
                grade = 2;
                break;
            case 2:
                grade = 3;
                Debug.Log(grade);
                break;
            case 3:
                grade = 4;
                break;
            case 4:
                grade = 5;
                Debug.Log(grade);
                break;
            case 5:
                grade = 6;
                break;
            default:
                grade = 1;
                break;
        }
    }

    private IEnumerator Register(string _email, string _password, string _username, int _grade)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {   
                //Code here updates firebase realtime database
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    Debug.Log("username: " + _username + " User ID : " + User.UserId + " Primary Lvl: " + _grade);
                    
                    var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
                    // Code to create user
                    User user2 = new User(User.UserId, 0, _grade);
                    user2.setId(User.UserId);
                    string result = linktoUserGet.createUser(url_user, user2);
                    Debug.Log(User.UserId + " created");
                    Debug.Log(result);
                    ClearRegisterFeilds();
                }
            }
        }
    }

    //private IEnumerator UpdateUsernameAuth(string _username)
    //{
    //    //Create a user profile and set the username
    //    UserProfile profile = new UserProfile { DisplayName = _username };

    //    //Call the Firebase auth update user profile function passing the profile with the username
    //    var ProfileTask = User.UpdateUserProfileAsync(profile);
    //    //Wait until the task completes
    //    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

    //    if (ProfileTask.Exception != null)
    //    {
    //        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
    //    }
    //    else
    //    {
    //        //Auth username is now updated
    //    }
    //}

    //private IEnumerator UpdateUsernameDatabase(string _username)
    //{
    //    //Set the currently logged in user username in the database
    //    var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

    //    yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

    //    if (DBTask.Exception != null)
    //    {
    //        Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
    //    }
    //    else
    //    {
    //        //Database username is now updated
    //    }
    //}
}
