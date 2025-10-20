using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SpriteGlow;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject.SpaceFighter;

public class BuildingView : MonoBehaviour
{
    public bool ReadyAcceptItem => _readyAcceptItem;

    [SerializeField] private GameObject buildingInfoPanel;
    [SerializeField] private GameObject buildingDescriptionPanel;
    [SerializeField] private SpriteRenderer buildingImage;
    [SerializeField] private Sprite unbuiltBuilding; 
    [SerializeField] private Sprite builtBuilding;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private CancellationTokenSource _cts = new();
    private bool _readyAcceptItem = false;
    private bool _timerIsRunning = false;
    private bool _isCompleted = false;
    private bool _isUsed = false;
    //   [SerializeField] private GameObject _glow;
    //    private InteractibleGlow _interactibleGlow;
    //   private SpriteGlowEffect _spriteGlow;
    private void Awake()
    {
        buildingImage = GetComponentInParent<SpriteRenderer>();
        buildingDescriptionPanel.SetActive(true);
        buildingInfoPanel.SetActive(true);
        
        buildingDescriptionPanel.transform.localScale = Vector3.zero;
        buildingInfoPanel.transform.localScale = Vector3.zero;
        
        canvasGroup.alpha = 0;
    }

    public void CompleteBuild() {
        
        _isCompleted = true;
        //      _glow.SetActive(false);
        buildingImage.sprite = builtBuilding;
    }
    
    public void SetDestroySprite()
    {
        buildingImage.sprite = unbuiltBuilding;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isCompleted)
        {
            PlayerOrchestrator player = other.GetComponent<PlayerOrchestrator>();
            if (!player.IsMove && !_timerIsRunning)
            {
                ShowView(player).Forget();
                _timerIsRunning = true;
            }
            if (player.IsMove && _timerIsRunning)
            {
                HideView();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isUsed = false;
        }
    }

    private async UniTask ShowView(PlayerOrchestrator player)
    {
        canvasGroup.alpha = 1;
        while (!player.IsMove)
        {
            _isUsed = true;
            if (_isCompleted) break;

            await Timer(0.2f);
            if (_isCompleted || player.IsMove) break;
             buildingInfoPanel.transform.DOScale(Vector3.one, 0.25f);
            _readyAcceptItem = true;
            if (_isCompleted || player.IsMove) break;
            await Timer(0.5f);
            if (_isCompleted || player.IsMove) break;
            await buildingDescriptionPanel.transform.DOScale(Vector3.one, 0.25f).AsyncWaitForCompletion();
        }
    }

    private void HideView()
    {
        buildingDescriptionPanel.transform.DOScale(Vector3.zero, 0.5f);
        buildingInfoPanel.transform.DOScale(Vector3.zero, 0.5f);
        Debug.Log("панель скрыта");
        _readyAcceptItem = false;
        _timerIsRunning = false;
    }

    private async UniTask Timer(float time)
    {
        float currentTime = 0;
        while (currentTime <= time)
        {
         await UniTask.Delay(TimeSpan.FromSeconds(0.1));
            currentTime += 0.1f;
        }
    }
}
