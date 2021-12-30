using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float answerDelayWrong;
    [SerializeField] private LayerMask cellMask;

    [SerializeField] private SessionController sessionController;
    [SerializeField] private QuestionController questionController;

    public bool HandleInput { get; set; }

    private Camera mainCamera;
    private bool isDelay;

    private void Awake()
    {
        mainCamera = Camera.main;
        sessionController.OnNewGameStarted.AddListener(delegate { HandleInput = true; isDelay = false; });
        sessionController.OnGameEnded.AddListener(delegate { HandleInput = false; });
        questionController.OnRightAnswer.AddListener(delegate { isDelay = true; });
        sessionController.OnRoundChanged.AddListener(delegate { isDelay = false; });
        questionController.OnWrongAnswer.AddListener(AnswerDelayWrong);
    }

    private void AnswerDelayWrong(string answer)
    {
        isDelay = true;
        DOTween.Sequence()
            .AppendInterval(answerDelayWrong)
            .AppendCallback(delegate { isDelay = false; });
    }

    private void Update()
    {
        if (HandleInput && !isDelay)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Collider2D coll = Physics2D.OverlapPoint(pos, cellMask);
                if (coll != null)
                    if (coll.TryGetComponent(out Cell cell))
                        cell.Clicked();
            }
        }
    }
}
