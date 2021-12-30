using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Round Data", menuName = "Round Data")]
public class RoundData : ScriptableObject
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public int Rows => rows;
    public int Columns => columns;
    public int ItemCount { get { return Rows * Columns; } }
}
