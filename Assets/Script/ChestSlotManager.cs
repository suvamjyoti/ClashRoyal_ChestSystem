using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestSlotManager : MonoBehaviour
{
    private ChestSlot[] chestSlots;

    [SerializeField] private int NoOfChestAllowedToQueue;

    #region Singelton

    private static ChestSlotManager _instance;

    public static ChestSlotManager instance { get { return _instance; } }

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

}
