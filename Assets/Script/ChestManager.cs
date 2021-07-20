using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestManager : MonoBehaviour
{
    private ChestSlot[] chestSlots;

    [SerializeField] private int NoOfChestAllowedToQueue;

    [SerializeField] private ChestScriptableObject[] chestList;
    public Dictionary<ChestType,ChestScriptableObject> chestPoolDict;
    
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
    }



    private void OnSlotClicked(ChestSlot chestSlot)
    {
        //open chest slot depending on the chest slot clicked


    }

    public void PushChestIntoSlot(ChestScriptableObject chest)
    {
        //check if any other chest open timer is on

        //check if more then allowed no of closed chest are already in queue

        //push into any empty slot


    }

}
