using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MultiQuiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    Question currentQn;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    [Header("ExtendTime Powerup")]
    [SerializeField] GameObject extendTimeButton;
    int extendTimeCount;

    [Header("ShowHint Powerup")]
    [SerializeField] GameObject showHintButton;
    int showHintCount;

    [Header("Skip Question Powerup")]
    [SerializeField] GameObject skipQuestionButton;
    int skipQuestionCount;

    [Header("Opponent Score")]
    [SerializeField] GameObject opponentScoreImage;
    int opponentScore;

    public bool isComplete;
    public bool useShowHint;

    List<Question> qnList;
    User currentUser;
    // User opponent;
    List<Item> userInventory;
    string url_qn = "http://localhost:3000/questions";
    string url_user = "http://localhost:3000/user";
    UserDao linktoUserGet;
    List<Question> completedQns;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.resetFields();

        // Need to make change userId accordingly
        string userId = "7HHcjbfJq1kD8VFMHHDq";
        linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        currentUser = linktoUserGet.getUser(url_user, userId);
        completedQns = currentUser.getCompletedQns();

        // Get user's inventory
        userInventory = currentUser.getInventory();
        // Get qn list from db
        var linktoQuestionGet = GameObject.Find("QuestionDao").GetComponent<QuestionDao>();
        var primaryLevel = currentUser.getPrimaryLevel();
        qnList = linktoQuestionGet.getQuestions(url_qn, primaryLevel);
        GetRandomQuestion();

        progressBar.maxValue = qnList.Count;
        progressBar.value = 0;
        extendTimeCount = GetExtendTimeCount(userInventory);
        showHintCount = GetShowHintCount(userInventory);
        skipQuestionCount = GetSkipQuestionCount(userInventory);
        // opponentScore = GetOpponentCorrectQuestionCount(opponent);
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                // Update user elo rating here
                UpdateUserPoints();
                UpdateUserQns();
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }

        if (useShowHint)
        {
            questionText.text = string.Format("{0}\n{1}", currentQn.GetQuestion(), currentQn.GetHint());
            useShowHint = false;
        }

        DisplayExtendTime();
        DisplayShowHint();
        DisplaySkipQuestion();
        DisplayOpponentScore();
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQn.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.SaveQuestionGotCorrect(currentQn);
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQn.GetCorrectAnswerIndex();
            string correctAnswer = currentQn.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was:\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.SaveQuestionGotWrong(currentQn);
        }
    }

    void GetNextQuestion()
    {
        if (qnList.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, qnList.Count);
        currentQn = qnList[index];

        if (qnList.Contains(currentQn))
        {
            qnList.Remove(currentQn);
        }

        if (completedQns.Count == 0)
        {
            completedQns.Add(currentQn);
            return;
        }
        else
        {
            foreach (var qns in completedQns)
            {
                if (string.Equals(qns.GetQuestion(), currentQn.GetQuestion()))
                {
                    return;
                }
            }
            completedQns.Add(currentQn);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQn.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQn.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void DisplayExtendTime()
    {
        Text extendTimeText = extendTimeButton.GetComponentInChildren<Text>();
        extendTimeText.text = "Extend Time = " + extendTimeCount;
    }

    public void OnExtendTimeSelected()
    {
        if (extendTimeCount > 0)
        {
            extendTimeCount -= 1;
            timer.ActivateExtendTime();
            // Update the user's extend time power-ups in inventory
            foreach (Item item in userInventory)
            {
                if (string.Equals(item.getItemName(), "Extend Time"))
                {
                    userInventory.Remove(item);
                    break;
                }
            }
            currentUser.setInventory(userInventory);
        }
    }

    void DisplayShowHint()
    {
        Text showHintText = showHintButton.GetComponentInChildren<Text>();
        showHintText.text = "Show Hint = " + showHintCount;
    }

    public void OnShowHintSelected()
    {
        if (showHintCount > 0)
        {
            useShowHint = true;
            showHintCount -= 1;
            // Update the user's show hint (tips) power-ups in inventory
            foreach (Item item in userInventory)
            {
                if (string.Equals(item.getItemName(), "Tips"))
                {
                    userInventory.Remove(item);
                    break;
                }
            }
            currentUser.setInventory(userInventory);
        }
    }

    void DisplaySkipQuestion()
    {
        Text skipQuestionText = skipQuestionButton.GetComponentInChildren<Text>();
        skipQuestionText.text = "Skip Qn = " + skipQuestionCount;
    }

    public void OnSkipQuestionSelected()
    {
        if (skipQuestionCount > 0)
        {
            skipQuestionCount -= 1;
            scoreKeeper.SaveQuestionGotCorrect(currentQn);
            scoreKeeper.IncrementCorrectAnswers();
            timer.loadNextQuestion = true;
            // Update the user's skip question power-ups in inventory
            foreach (Item item in userInventory)
            {
                if (string.Equals(item.getItemName(), "Question Skip"))
                {
                    userInventory.Remove(item);
                    break;
                }
            }
            currentUser.setInventory(userInventory);
        }
    }

    void DisplayOpponentScore()
    {
        Text opponentScoreText = opponentScoreImage.GetComponentInChildren<Text>();
        // opponentScore = GetOpponentCorrectQuestionCount(opponent);
        opponentScoreText.text = "Opponent's Score: " + opponentScore;
    }

    private void UpdateUserPoints()
    {
        int score = currentUser.getPoints();
        score += scoreKeeper.CalculatePoints();
        currentUser.setPoints(score);
    }

    private void UpdateUserQns()
    {
        currentUser.setCompletedQns(completedQns);
        int correctAns = currentUser.getCorrectQns() + scoreKeeper.GetCorrectAnswers();
        currentUser.setCorrectQns(correctAns);
        int wrongAns = currentUser.getWrongQns() + scoreKeeper.GetWrongAnswers();
        currentUser.setWrongQns(wrongAns);
    }

    private int GetExtendTimeCount(List<Item> inventory)
    {
        int count = 0;
        foreach (Item item in inventory)
        {
            if (string.Equals(item.getItemName(), "Time Extend"))
            {
                count += 1;
            }
        }
        return count;
    }

    private int GetShowHintCount(List<Item> inventory)
    {
        int count = 0;
        foreach (Item item in inventory)
        {
            if (string.Equals(item.getItemName(), "Tips"))
            {
                count += 1;
            }
        }
        return count;
    }

    private int GetSkipQuestionCount(List<Item> inventory)
    {
        int count = 0;
        foreach (Item item in inventory)
        {
            if (string.Equals(item.getItemName(), "Question Skip"))
            {
                count += 1;
            }
        }
        return count;
    }

    //private int GetOpponentCorrectQuestionCount(User Opponent)
    //{
    //    return 0;
    //}
}
