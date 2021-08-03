using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public event Action OnUnlocked;



    #region Singelton

    private static EventManager _instance;
    public static EventManager instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != this && _instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void InvokeUnlocked()
    {
        OnUnlocked?.Invoke();
    }

    #endregion

}
