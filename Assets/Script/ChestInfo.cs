using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ChestInfo : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coinTextField;
    [SerializeField] private TextMeshProUGUI gemTextField;

    [SerializeField] private Button chestInfoButton;
    [SerializeField] private TextMeshProUGUI chestInfoText;

    //[SerializeField] private Button openUsingGemButton;
    [SerializeField] private Button closeButton;


    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gemText;

    [SerializeField] private Image chestImage;
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private ChestCollect chestCollect;

    private int gemCost;
    private float timer;
    private Coroutine gemCostCoroutine;


    private int m_coinLowerBound;
    private int m_coinUpperBound;
    private int m_gemsLowerBound;
    private int m_gemsUpperBound;
    private ChestState m_chestCurrentState;

    
    //constructor
    public void Initialise(ChestSlot chestSlot)
    {
        m_coinLowerBound = chestSlot.chestConfig.coinLowerBound;
        m_coinUpperBound = chestSlot.chestConfig.coinUpperBound;

        m_gemsLowerBound = chestSlot.chestConfig.gemsLowerBound;
        m_gemsUpperBound = chestSlot.chestConfig.gemsUpperBound;

        chestImage.sprite = chestSlot.chestConfig.chestSprite;

        m_chestCurrentState = chestSlot.chestConfig.chestCurrentState;

        timer = chestSlot.chestConfig.timer;

        EventManager.instance.OnUnlocked += OnChestTimerComplete;

        timerText.text = timer.ToString();

        chestInfoButton.gameObject.SetActive(true);

        init();
    }

    void init()
    {

        //initialising the text field
        coinTextField.text = $"{m_coinLowerBound} - {m_coinUpperBound} ";
        gemTextField.text = $"{m_gemsLowerBound} - {m_gemsUpperBound} ";


        switch (m_chestCurrentState)
        {
            case ChestState.Locked:
                initialiseButton(onClickStartTimer, "Start Timer");
                break;
            case ChestState.Unlocked:
                initialiseButton(onClickCollectChest, "Collect Chest");
                break;
            case ChestState.Opening:
                //
                break;
        }

        closeButton.onClick.AddListener(onClose);
        timerManager.gameObject.SetActive(false);
    }

    private void onClose()
    {
        this.gameObject.SetActive(false);
    }


    private void onClickCollectChest()
    {

    }

    private void onClickStartTimer()
    {
        initialiseButton(onClickOpenUsingGem,"Open using Chest");
        
        
        timerText.gameObject.SetActive(false);

        //call start timer function
        timerManager.gameObject.SetActive(true);
        timerManager.InitialiseTimer(timer);

        //openUsingGemButton.gameObject.SetActive(true);
        gemText.gameObject.SetActive(true);

        //update gem cost every 1 minute
        gemCostCoroutine = StartCoroutine(updateGemCost());

    }

    private void initialiseButton(UnityAction callBack,string buttonName)
    {
        chestInfoButton.onClick.RemoveAllListeners();
        chestInfoButton.onClick.AddListener(callBack);
        chestInfoText.text = buttonName;
    }

    private IEnumerator updateGemCost()
    {
        while (timerManager.getRemainingTime()>60)
        {
            gemCost = (int)(timerManager.getRemainingTime() / 30);
            gemText.text = gemCost.ToString();
            yield return new WaitForSeconds(60);
        }
    }

    private void onClickOpenUsingGem()
    {
        //deduct gemCostToOpen from the total no of remaining gem
        UiManager.Instance.updateGemValue(-gemCost);
        if (gemCostCoroutine != null)
        {
            StopCoroutine(gemCostCoroutine);
        }

        //chest state open

        onClose();
    }

    private void OnChestTimerComplete()
    {
        initialiseButton(OnClickCollectChest, "Collect Chest");
    }

    private void OnClickCollectChest()
    {
        //open a colect ui screen
        chestCollect.gameObject.SetActive(true);

        int _coin = Random.Range(m_coinLowerBound, m_coinUpperBound);
        int _gem = Random.Range(m_gemsLowerBound, m_gemsUpperBound);
        chestCollect.Initialise(_coin,_gem);
        
        Debug.Log($"coin received = {_coin}");
        Debug.Log($"gem received = {_gem}");

        this.gameObject.SetActive(false);
    }

}
