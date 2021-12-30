using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIRestartHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup screenGroup;
    [SerializeField] private Button restartButton;
    [SerializeField] private SessionController sessionController;
    [SerializeField] private UILoadingScreen loadingScreen;

    private void Awake()
    {
        gameObject.SetActive(false);
        sessionController.OnGameEnded.AddListener(ShowScreen);
        restartButton.onClick.AddListener(OnRestart);
    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);
        restartButton.interactable = true;
        screenGroup.DoFadeIn(1);
    }

    public Tween HideScreen()
    {
        restartButton.interactable = false;
        return DOTween.Sequence()
                .Append(screenGroup.DoFadeOut(1))
                .AppendCallback(delegate { gameObject.SetActive(false); });
    }

    public void OnRestart()
    {
        DOTween.Sequence()
            .AppendCallback(delegate { restartButton.interactable = false; })
            .Append(loadingScreen.Show())
            .AppendInterval(1)
            .AppendCallback(sessionController.ResetGame)
            .AppendCallback(delegate { gameObject.SetActive(false); })
            .Append(loadingScreen.Hide())
            .AppendCallback(sessionController.StartNewGame);
    }
}
