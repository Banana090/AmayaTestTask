using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemRenderer;

    public string Identifier { get; private set; }

    public CellClickedEvent OnClick { get; private set; } = new CellClickedEvent();

    public void SetItem(BundleData.Item item)
    {
        Identifier = item.Identifier;
        itemRenderer.sprite = item.Sprite;
    }

    public void Bounce()
    {
        transform.DoBounce();
    }

    public void TweenRight()
    {
        itemRenderer.transform.DoBounce();
    }

    public void TweenWrong()
    {
        itemRenderer.transform.DoInBounceShake();
    }

    public void Clicked()
    {
        OnClick?.Invoke(Identifier);
    }

    /// <summary>
    /// Вот вместо этой хуеты стоит использовать System.Action или делегаты/ивенты.
    /// </summary>
    public class CellClickedEvent : UnityEvent<string> { }
}
