﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestSlot : MonoBehaviour
{

    private bool _isEmpty = true;
    public bool isEmpty { get{ return _isEmpty;} }
    
    
    [SerializeField] private Image chestSprite;

    [HideInInspector] public ChestScriptableObject chestConfig;

    
    public void AddChest(ChestScriptableObject chest)
    {
        Debug.Log("adding chest to chest slot");
        _isEmpty = false;

        chestConfig = chest;
        chestSprite.enabled = true;

        //setting chest sprite
        chestSprite.sprite = chest.chestSprite;
    }


}
