using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameGrid : MonoBehaviour
{
    private const float CELL_SIZE = 1;
    private const float CELL_OFFSET = 0.5f;

    [SerializeField] private QuestionController questionController;
    [SerializeField] private SessionController sessionController;
    [SerializeField] private Cell cellPrefab;

    private Transform m_Transform;
    private Cell[] cells;

    public Cell.CellClickedEvent OnCellClicked { get; private set; } = new Cell.CellClickedEvent();

    private int index;

    private void Awake()
    {
        sessionController.OnNewGameStarted.AddListener(BounceCells);
        sessionController.OnGameReset.AddListener(ClearGrid);
        questionController.OnRightAnswer.AddListener(OnRightAnswer);
        questionController.OnWrongAnswer.AddListener(OnWrongAnswer);
        m_Transform = transform;
    }

    private void OnRightAnswer(string id) => FindCellWithID(id).TweenRight();
    private void OnWrongAnswer(string id) => FindCellWithID(id).TweenWrong();

    private void ClearGrid()
    {
        if (cells != null)
            for (int i = 0; i < cells.Length; i++)
                Destroy(cells[i].gameObject);
        cells = null;
    }

    private Cell FindCellWithID(string id)
    {
        return cells.First(c => c.Identifier == id);
    }

    public void CreateGrid(RoundData data, List<BundleData.Item> items)
    {
        ClearGrid();

        cells = new Cell[data.ItemCount];

        Vector2 start = new Vector2(data.Columns / -2.0f, data.Rows / -2.0f);
        for (int row = 0; row < data.Rows; row++)
        {
            for (int column = 0; column < data.Columns; column++)
            {
                Cell c = Instantiate(cellPrefab, start + new Vector2(column * CELL_SIZE + CELL_OFFSET, row * CELL_SIZE + CELL_OFFSET), Quaternion.identity, m_Transform);
                cells[row * data.Columns + column] = c;
                c.OnClick.AddListener(OnCellClick);
            }
        }

        FillGrid(items);
    }

    private void FillGrid(List<BundleData.Item> items)
    {
        index = 0;
        int length = items.Count < cells.Length ? items.Count : cells.Length;
        for (int i = 0; i < length; i++)
        {
            if (index < cells.Length)
                cells[index++].SetItem(items[i]);
        }
    }

    private void BounceCells()
    {
        for (int i = 0; i < cells.Length; i++)
            cells[i].Bounce();
    }

    private void OnCellClick(string identifier)
    {
        OnCellClicked?.Invoke(identifier);
    }
}
