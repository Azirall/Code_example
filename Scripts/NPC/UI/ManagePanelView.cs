using System;
using UnityEngine;
using Zenject;

public class ManagePanelView : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private ManageBoard _board;
    
    [Inject]
    public void Construct(ManageBoard board)
    {
        _board = board;
    }

    private void Awake()
    {
        _board.Show += ShowPanel;
        _board.Hide += HidePanel;
    }

    private void ShowPanel() => panel.SetActive(true);
    private void HidePanel() => panel.SetActive(false);

    private void OnDestroy()
    {
        _board.Show -= ShowPanel;
        _board.Hide -= HidePanel;
    }
}
