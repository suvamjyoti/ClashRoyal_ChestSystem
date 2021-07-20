using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GenerateChest : MonoBehaviour
{
    private Button generateButton;

    void Start()
    {
        generateButton = GetComponent<Button>();
        generateButton.onClick.AddListener(GenerateChestOnClick);
    }

    private void GenerateChestOnClick()
    {

        //get the chest from poll based on chest type
        ChestScriptableObject chest = ChestManager.instance.chestPoolDict[GetRandomChestType()];

        ChestManager.instance.PushChestIntoSlot(chest);
    }


    private ChestType GetRandomChestType()
    {
        
        int temp = Random.Range(0, 100);
        
        if (temp >= 95)                     //5% chance chest will be legendary
        {
            return ChestType.Legendary;
        }
        else if (temp >=80 && temp<95)      //15% chance chest will be epic
        {
            return ChestType.Epic;
        }
        else if(temp>=50 && temp<80)        //30% chance chest will be rare
        {
            return ChestType.Rare;
        }
        else                                //50% chance chest will be common
        {
            return ChestType.Common;
        }
    }

}
