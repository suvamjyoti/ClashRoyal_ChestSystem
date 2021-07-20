using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlot : MonoBehaviour
{

    private bool _isEmpty = true;
    public bool isEmpty { get{ return _isEmpty;} }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddChest()
    {
        _isEmpty = false;
    }
}
