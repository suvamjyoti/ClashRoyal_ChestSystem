using System;
using UnityEngine;
using UnityEngine.UI;


public class ChestSlot : MonoBehaviour
{

    private bool _isEmpty = true;
    public bool isEmpty { get{ return _isEmpty;} }

    private bool _isOpening = false;
    public bool isOpening { get { return _isOpening; } }


    public ChestState currentChestState = ChestState.Locked; 
    
    
    [SerializeField] private Image chestSprite;
    [SerializeField] private Image chestSlotBackground;

    [SerializeField] private TimerManager timerManager;

    [HideInInspector] public ChestScriptableObject chestConfig;

    private void Start()
    {
    }

    public void AddChest(ChestScriptableObject chest)
    {
        _isEmpty = false;

        chestConfig = chest;
        chestSprite.enabled = true;

        //setting chest sprite
        chestSprite.sprite = chest.chestSprite;
    }

    public void OnChestCollected()
    { 
        _isEmpty = true;
        chestSprite.sprite = null;
        chestSprite.enabled = false;
        chestConfig = null;
        currentChestState = ChestState.Locked;
        setBackgroundAlpha(0);
    }

    public void OnChestUnlocked()
    {
        _isOpening = false;
        setBackgroundAlpha(1);
    }

    public void startTimer()
    {
        _isOpening = true;
        //call start timer function
        timerManager.gameObject.SetActive(true);
        timerManager.InitialiseTimer(chestConfig.timer);
    }

    public void setBackgroundAlpha(float alphaValue)
    {
        Color tempColor = chestSlotBackground.color;
        tempColor.a = alphaValue;
        chestSlotBackground.color = tempColor;
    }

    public void StartChestTimer()
    {

    }


}
