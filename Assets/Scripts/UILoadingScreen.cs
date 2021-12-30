using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILoadingScreen : MonoBehaviour
{
    [SerializeField] private Image background;

    public Tween Show()
    {
        return background.DoFadeIn(1);
    }

    public Tween Hide()
    {
        return background.DoFadeOut(1);
    }
}
