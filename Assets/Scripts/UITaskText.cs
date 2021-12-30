using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITaskText : MonoBehaviour
{
    [SerializeField] private QuestionController questionController;
    [SerializeField] private SessionController sessionController;
    [SerializeField] private Text answerText;


    private void Awake()
    {
        sessionController.OnGameReset.AddListener(OnReset);
        sessionController.OnNewGameStarted.AddListener(FadeIn);
        questionController.OnNewAnswerCreated.AddListener(SetText);
    }

    private void OnReset()
    {
        answerText.color = new Color(answerText.color.r, answerText.color.g, answerText.color.b, 0);
    }

    private void FadeIn()
    {
        answerText.DoFadeIn(0.8f);
    }

    private void SetText(string answer)
    {
        answerText.text = "Find " + answer;
    }
}
