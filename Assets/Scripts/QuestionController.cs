using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private BundleData[] bundleDatas;
    [SerializeField] private GameGrid gameGrid;
    [SerializeField] private SessionController sessionController;

    private Dictionary<BundleData, List<BundleData.Item>> allowedQuestions;
    private List<BundleData.Item> poll = new List<BundleData.Item>();

    public RightAnswerEvent OnRightAnswer { get; private set; } = new RightAnswerEvent();
    public WrongAnswerEvent OnWrongAnswer { get; private set; } = new WrongAnswerEvent();
    public NewAnswerCreatedEvent OnNewAnswerCreated { get; private set; } = new NewAnswerCreatedEvent();

    private string answer;

    private void Awake()
    {
        gameGrid.OnCellClicked.AddListener(OnAnswer);
        sessionController.OnGameReset.AddListener(ResetGame);
    }

    private void OnAnswer(string id)
    {
        if (id == answer)
            OnRightAnswer?.Invoke(id);
        else
            OnWrongAnswer?.Invoke(id);
    }

    public void ResetGame()
    {
        allowedQuestions = new Dictionary<BundleData, List<BundleData.Item>>();
        for (int i = 0; i < bundleDatas.Length; i++)
            allowedQuestions.Add(bundleDatas[i], bundleDatas[i].Data.ToList());
    }

    public void CreateQuestion(RoundData round)
    {
        BundleData bundle = GetRandomBundleWithAtLeast(round.ItemCount);
        BundleData.Item answer = GetRandomItem(bundle);
        
        poll.Clear();
        poll.Add(answer);

        while (poll.Count < round.ItemCount)
            poll.Add(GetRandomItem(bundle));

        poll.Shuffle();
        gameGrid.CreateGrid(round, poll);

        this.answer = answer.Identifier;
        OnNewAnswerCreated?.Invoke(answer.Identifier);
    }

    private BundleData.Item GetRandomItem(BundleData bundle)
    {
        List<BundleData.Item> items = allowedQuestions[bundle];
        BundleData.Item answer = items[Random.Range(0, items.Count)];
        items.Remove(answer);
        return answer;
    }

    private BundleData GetRandomBundleWithAtLeast(int questions)
    {
        int bundles = 0;
        foreach (var pair in allowedQuestions)
        {
            if (pair.Value.Count < questions) continue;
            bundles++;
        }

        if (bundles == 0)
            return null;

        float chance = 1.0f / bundles;
        float chosen = Random.value;
        float count = 0;
        foreach (var pair in allowedQuestions)
        {
            if (pair.Value.Count < questions) continue;
            count += chance;
            if (chosen <= count)
                return pair.Key;
        }

        throw new System.Exception("Unable to find chosen pair. Never happens");
    }


    public class RightAnswerEvent : UnityEvent<string> { }
    public class WrongAnswerEvent : UnityEvent<string> { }
    public class NewAnswerCreatedEvent : UnityEvent<string> { }
}
