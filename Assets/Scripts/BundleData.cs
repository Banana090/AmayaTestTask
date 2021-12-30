using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Bundle Data", menuName = "Bundle Data")]
public class BundleData : ScriptableObject
{
    [SerializeField] private Item[] data;
    public Item[] Data => data;

    [Serializable]
    public class Item
    {
        [SerializeField] private string identifier;
        [SerializeField] private Sprite sprite;

        public string Identifier => identifier;
        public Sprite Sprite => sprite;
    }
}
