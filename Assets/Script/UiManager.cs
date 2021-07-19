using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    private static UiManager _inst;
    public static UiManager Instance
    {
        get
        {
            return _inst;
        }
    }

    private void Awake()
    {
        if (_inst == null)
        {
            _inst = this;
        }
    }

    public void pushController(GameObject ControllerPrefab,Transform trasform) 
    {
        GameObject controller = Instantiate(ControllerPrefab, transform);
        transform.parent = controller.transform;
    }

}
