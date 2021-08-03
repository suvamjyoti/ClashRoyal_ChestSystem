using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private int coins;
    [SerializeField] private int gems;
    

    #region SINGELTON

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

        gemText.text = gems.ToString();
        coinText.text = coins.ToString();
    }

    #endregion

    public void updateGemValue(int gemDiference)
    {
        gems += gemDiference;
        gemText.text = gems.ToString();
    }

}
