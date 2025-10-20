using System;
using TMPro;
using UnityEngine;
using Zenject;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayCountText;
    private GameCycleOrchestrator _gameCycleOrchestrator;

    [Inject]
    public void Construct(GameCycleOrchestrator orchestrator)
    {
        _gameCycleOrchestrator = orchestrator;
    }

    private void Awake()
    {
        _gameCycleOrchestrator.DayCounterChange += UpdateText;
    }

    private void UpdateText(int count)
    {
        dayCountText.text = $"день: {count}";
    }
}
