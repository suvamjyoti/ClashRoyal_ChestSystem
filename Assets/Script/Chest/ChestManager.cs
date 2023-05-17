using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChestManager : MonoBehaviour
{
    private ChestSlot[] chestSlots;

    [SerializeField] private int NoOfChestAllowedToQueue;
    [SerializeField] private int NoOfChestAllowedToOpenSimultaniously;


    [SerializeField] private ChestScriptableObject[] chestList;
    public Dictionary<ChestType,ChestScriptableObject> chestPoolDict;

    [SerializeField] private ChestInfo chestInfo;

   

    private bool isChestOpening = false;
    
    #region Singelton

    private static ChestManager _instance;

    public static ChestManager instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance!=this && _instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }


        //initialise chest slot list
        chestSlots = transform.GetComponentsInChildren<ChestSlot>();

        //initialise chest dictionary
        chestPoolDict = new Dictionary<ChestType, ChestScriptableObject>();
        foreach(ChestScriptableObject chest in chestList)
        {
            chestPoolDict.Add(chest.chestType,chest);
        }
    }

    #endregion


    private void Start()
    {
        //add listener to buttons
        foreach(ChestSlot chestSlot in chestSlots)
        {
            Button button = chestSlot.GetComponent<Button>();
            button.onClick.AddListener(delegate { OnSlotClicked(chestSlot);});
        }

        //setFunction when a chest unlocks
        EventManager.instance.OnUnlocked += OnChestTimerComplete;
    }

    private void OnChestTimerComplete()
    {
        ChestSlot m_chestSlot = null;

        foreach (ChestSlot chest in chestSlots)
        {
            
            
            if (!chest.isEmpty && chest.isOpening)
            {
                //if chest slot not empty
                m_chestSlot = chest;
            }
        }

        if (m_chestSlot != null)
        {
            m_chestSlot.currentChestState = ChestState.Unlocked;
            m_chestSlot.OnChestUnlocked();
        }
        else
        {
            Debug.LogError("Chest not found");
        }
    }

    private void OnSlotClicked(ChestSlot chestSlot)
    {
        Debug.Log("Here");
        if (chestSlot.isEmpty)
        {
            return;
        }

        chestInfo.Initialise(chestSlot);
        chestInfo.gameObject.SetActive(true);

    }

    public void PushChestIntoSlot(ChestScriptableObject chest)
    {
        Debug.Log("manager got chest");

        int noOfChestInQueue = CheckNoOfChestQueue();

        //check if more then allowed no of closed chest are already in queue
        if(noOfChestInQueue >= NoOfChestAllowedToQueue)
        {
            //show toaster stating not allowed to generate
            Debug.LogError("queue is full");
            return;
        }
        else
        {
            Debug.Log("finding empty chest slot");
            //push into the first empty slot found
            foreach (ChestSlot chestSlot in chestSlots)
            {
                if (chestSlot.isEmpty)
                {
                    Debug.Log("pushing to slot");
                    chestSlot.AddChest(chest);
                    return;
                }
            }
        }
    }

    private int CheckNoOfChestQueue()
    {
        int nos=0;

        foreach(ChestSlot chestSlot in chestSlots)
        {
            nos = (chestSlot.isEmpty) ? nos : nos+1;
        }

        return nos;
    }

    public bool CanStartOpeningChest()
    {
        int nos = 0;

        foreach (ChestSlot chestSlot in chestSlots)
        {
            nos = (chestSlot.isOpening) ? nos+1 : nos ;
        }

        return (nos>=NoOfChestAllowedToOpenSimultaniously)? false : true;
    }

}
