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


    [SerializeField] private TextMeshProUGUI textField;
    
    [SerializeField] private Image chestImage;
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private ChestCollect chestCollect;
    [SerializeField] private int updateGemCostAfter = 2;        //in sec

    private int gemCost=0;
    private float timer=0;
    private Coroutine gemCostCoroutine = null;


    private int m_coinLowerBound;
    private int m_coinUpperBound;
    private int m_gemsLowerBound;
    private int m_gemsUpperBound;
    private ChestSlot m_chestSlot;
    private ChestState m_chestCurrentState;

    

    
    //constructor
    public void Initialise(ChestSlot chestSlot)
    {
        m_chestSlot = chestSlot;
        m_coinLowerBound = chestSlot.chestConfig.coinLowerBound;
        m_coinUpperBound = chestSlot.chestConfig.coinUpperBound;
        m_gemsLowerBound = chestSlot.chestConfig.gemsLowerBound;
        m_gemsUpperBound = chestSlot.chestConfig.gemsUpperBound;

        chestImage.sprite = chestSlot.chestConfig.chestSprite;
        m_chestCurrentState = chestSlot.chestConfig.chestCurrentState;
        timer = chestSlot.chestConfig.timer;


        EventManager.instance.OnUnlocked += OnChestTimerComplete;


        textField.text = timer.ToString();
        chestInfoButton.gameObject.SetActive(true);

        init();
    }

    void init()
    {

        //initialising the text field
        coinTextField.text = $"{m_coinLowerBound} - {m_coinUpperBound} ";
        gemTextField.text = $"{m_gemsLowerBound} - {m_gemsUpperBound} ";


        switch (m_chestSlot.currentChestState)
        {
            case ChestState.Locked:
                initialiseButton(onClickStartTimer, "Start Timer");
                break;
            case ChestState.Unlocked:
                initialiseButton(OnClickCollectChest, "Collect Chest");
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

    private void onClickStartTimer()
    {
        if (ChestManager.instance.CanStartOpeningChest())
        {
            initialiseButton(onClickOpenUsingGem, "Open using Gems");

            m_chestSlot.currentChestState = ChestState.Opening;
            m_chestSlot.startTimer();
            textField.gameObject.SetActive(false);

            //call start timer function
            timerManager.gameObject.SetActive(true);
            timerManager.InitialiseTimer(timer);

            //update gem cost every 1 minute
            gemCostCoroutine = StartCoroutine(updateGemCost());
        }
        else
        {
            Debug.LogError("Another chest is currently opening");
        }
    }

    private void initialiseButton(UnityAction callBack,string buttonName)
    {
        chestInfoButton.onClick.RemoveAllListeners();
        chestInfoButton.onClick.AddListener(callBack);
        chestInfoText.text = buttonName;
    }

    private IEnumerator updateGemCost()
    {

        textField.gameObject.SetActive(true);
        textField.text = (timerManager.getRemainingTime() / 2).ToString();

        while (timerManager.getRemainingTime()>2)
        {
            gemCost = (int)(timerManager.getRemainingTime() / 2);
            textField.text = gemCost.ToString();
            yield return new WaitForSeconds(updateGemCostAfter);
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

        EventManager.instance.InvokeUnlocked();
        OnChestTimerComplete();
    }

    private void OnChestTimerComplete()
    {
        textField.gameObject.SetActive(false);
        initialiseButton(OnClickCollectChest, "Collect Chest");
        m_chestSlot.currentChestState = ChestState.Unlocked;
        m_chestSlot.OnChestUnlocked();
    }

    private void OnClickCollectChest()
    {
        m_chestSlot.OnChestCollected();

        //open a colect ui screen
        chestCollect.gameObject.SetActive(true);

        int _coin = Random.Range(m_coinLowerBound, m_coinUpperBound);
        int _gem = Random.Range(m_gemsLowerBound, m_gemsUpperBound);
        chestCollect.Initialise(_coin,_gem);
        
        Debug.Log($"coin received = {_coin}");
        Debug.Log($"gem received = {_gem}");
        
        ResetLocalVariable();
        this.gameObject.SetActive(false);
    }

    private void ResetLocalVariable()
    {
        textField.gameObject.SetActive(true);
        gemCost = 0;
        timer = 0;
        gemCostCoroutine = null;
}

}
