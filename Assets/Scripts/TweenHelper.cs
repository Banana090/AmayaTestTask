using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class TweenHelper
{
    public static void DoBounce(this Transform t)
    {
        DOTween.Sequence()
            .Append(t.DOScale(0, 0))
            .Append(t.DOScale(1.2f, 0.2f))
            .Append(t.DOScale(0.95f, 0.2f))
            .Append(t.DOScale(1, 0.2f));
    }

    /// <summary>
    /// Unfinished
    /// </summary>
    public static void DoInBounceShake(this Transform t)
    {
		float shake = 0.15f;
		float time = 0.25f;
        float startX = t.position.x;
        DOTween.Sequence()
            .Append(t.DOMoveX(0, 0))
            .Append(t.DOMoveX(startX + shake * +0.0154f, time * 0.04f))
            .Append(t.DOMoveX(startX + shake * -0.0154f, time * 0.04f))
            .Append(t.DOMoveX(startX + shake * +0.0625f, time * 0.10f))
            .Append(t.DOMoveX(startX + shake * -0.0625f, time * 0.08f))
            .Append(t.DOMoveX(startX + shake * +0.2498f, time * 0.20f))
            .Append(t.DOMoveX(startX + shake * -0.2498f, time * 0.18f))
            .Append(t.DOMoveX(startX + shake * +0.5644f, time * 0.12f))
            .Append(t.DOMoveX(startX + shake * -0.5644f, time * 0.12f))
            .Append(t.DOMoveX(startX + shake * +0.0000f, time * 0.12f));
    }

    public static Tween DoFadeIn(this CanvasGroup target, float time)
    {
        target.alpha = 0;
        return target.DOFade(1, time);
    }

    public static Tween DoFadeOut(this CanvasGroup target, float time)
    {
        target.alpha = 1;
        return target.DOFade(0, time);
    }

    public static Tween DoFadeIn(this Graphic target, float time)
    {
        target.color = new Color(target.color.r, target.color.g, target.color.b, 0);
        return target.DOFade(1, time);
    }

    public static Tween DoFadeOut(this Graphic target, float time)
    {
        target.color = new Color(target.color.r, target.color.g, target.color.b, 0);
        return target.DOFade(0, time);
    }
}
