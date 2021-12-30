using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class SessionController : MonoBehaviour
{
    [SerializeField] private float newRoundDelay;

    [SerializeField] private RoundData[] rounds;
    [SerializeField] private QuestionController questionController;

    public UnityEvent OnNewGameStarted { get; private set; } = new UnityEvent();
    public UnityEvent OnGameEnded { get; private set; } = new UnityEvent();
    public UnityEvent OnGameReset { get; private set; } = new UnityEvent();
    public UnityEvent OnRoundChanged { get; private set; } = new UnityEvent();

    private int roundIndex;
    private string answer;

    private void Awake()
    {
        questionController.OnRightAnswer.AddListener(OnRightAnswer);
    }

    private void Start()
    {
        StartNewGame();
    }

    public void ResetGame()
    {
        roundIndex = 0;
        OnGameReset?.Invoke();
    }

    public void StartNewGame()
    {
        ResetGame();
        questionController.CreateQuestion(rounds[roundIndex]);
        OnNewGameStarted?.Invoke();
    }

    private void OnRightAnswer(string id)
    {
        DOTween.Sequence()
            .AppendInterval(newRoundDelay)
            .AppendCallback(StartNextRound);
    }

    private void StartNextRound()
    {
        roundIndex++;
        if (roundIndex < rounds.Length)
        {
            questionController.CreateQuestion(rounds[roundIndex]);
            OnRoundChanged?.Invoke();
        }
        else
        {
            OnGameEnded?.Invoke();
        }
    }
}
