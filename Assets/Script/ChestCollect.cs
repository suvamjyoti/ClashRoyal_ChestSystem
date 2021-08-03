using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChestCollect : MonoBehaviour
{
    public TextMeshProUGUI coinTextField;
    public TextMeshProUGUI gemTextField;
    public Button collectButton; 

    private int m_coinAmount;
    private int m_gemAmount;


    private void Start()
    {
        collectButton.onClick.AddListener(onClickCollect);   
    }


    public void Initialise(int coinAmount,int gemAmount)
    {
        m_coinAmount = coinAmount;
        m_gemAmount = gemAmount;

        coinTextField.text = coinAmount.ToString();
        gemTextField.text = gemAmount.ToString();
    }

    
    private void onClickCollect()
    {
        UiManager.Instance.updateGemValue(m_gemAmount);
        UiManager.Instance.updateCoinValue(m_coinAmount);

        this.gameObject.SetActive(false);
    }
}
