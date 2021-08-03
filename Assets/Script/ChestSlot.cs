using System;
using UnityEngine;
using UnityEngine.UI;


public class ChestSlot : MonoBehaviour
{

    private bool _isEmpty = true;
    public bool isEmpty { get{ return _isEmpty;} }
    
    
    [SerializeField] private Image chestSprite;

    [HideInInspector] public ChestScriptableObject chestConfig;


    public event Action OnUnlocked; 

    
    public void AddChest(ChestScriptableObject chest)
    {
        _isEmpty = false;

        chestConfig = chest;
        chestSprite.enabled = true;

        //setting chest sprite
        chestSprite.sprite = chest.chestSprite;
    }

    public void RemoveChest()
    {

    }

    public void StartChestTimer()
    {

    }

    public void OnChestUnlocked()
    {
        OnUnlocked?.Invoke();
    }


}
