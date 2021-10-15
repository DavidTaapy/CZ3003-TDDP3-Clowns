using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MultiQuiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

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
    int extendTimeNumber;

    [Header("ShowHint Powerup")]
    [SerializeField] GameObject showHintButton;
    int showHintNumber;

    [Header("Skip Question Powerup")]
    [SerializeField] GameObject skipQuestionButton;
    int skipQuestionNumber;

    [Header("Opponent Score")]
    [SerializeField] GameObject opponentScoreImage;
    int opponentScore;

    [Header("Auxiliary")]
    public bool isComplete;
    public bool useShowHint;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        extendTimeNumber = 2;
        showHintNumber = 2;
        skipQuestionNumber = 2;
        opponentScore = 3;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
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
            questionText.text = currentQuestion.GetHint();
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
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was:\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
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
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
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
        extendTimeText.text = "Extend Time = " + extendTimeNumber;
    }

    public void OnExtendTimeSelected()
    {
        if (extendTimeNumber > 0)
        {
            extendTimeNumber -= 1;
            timer.ActivateExtendTime();
        }
    }

    void DisplayShowHint()
    {
        Text showHintText = showHintButton.GetComponentInChildren<Text>();
        showHintText.text = "Show Hint = " + showHintNumber;
    }

    public void OnShowHintSelected()
    {
        if (showHintNumber > 0)
        {
            useShowHint = true;
            showHintNumber -= 1;
        }
    }

    void DisplaySkipQuestion()
    {
        Text skipQuestionText = skipQuestionButton.GetComponentInChildren<Text>();
        skipQuestionText.text = "Skip Qn = " + skipQuestionNumber;
    }

    public void OnSkipQuestionSelected()
    {
        if (skipQuestionNumber > 0)
        {
            skipQuestionNumber -= 1;
            scoreKeeper.IncrementCorrectAnswers();
            timer.loadNextQuestion = true;
        }
    }

    void DisplayOpponentScore()
    {
        Text opponentScoreText = opponentScoreImage.GetComponentInChildren<Text>();
        opponentScoreText.text = "Opponent's Score: " + opponentScore;
    }
}
