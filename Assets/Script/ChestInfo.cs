using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestInfo : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coinTextField;
    [SerializeField] private TextMeshProUGUI gemTextField;

    [SerializeField] private Button startTimerButton;
    [SerializeField] private Button openUsingGemButton;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gemText;

    [SerializeField] private Image chestImage;


    private int coinLowerBound;
    private int coinUpperBound;
    private int gemsLowerBound;
    private int gemsUpperBound;

    
    //constructor
    public ChestInfo(ChestScriptableObject chest)
    {
        coinLowerBound = chest.coinLowerBound;
        coinUpperBound = chest.coinUpperBound;

        gemsLowerBound = chest.gemsLowerBound;
        gemsUpperBound = chest.gemsUpperBound;

        chestImage.sprite = chest.chestSprite;

        timerText.text = chest.timer.ToString();

        init();
    }



    // Start is called before the first frame update
    void init()
    {

        //initialising the text field
        coinTextField.text = $"{coinLowerBound} - {coinUpperBound} ";
        gemTextField.text = $"{gemsLowerBound} - {gemsUpperBound} ";

        //adding listener to buttons
        startTimerButton.onClick.AddListener(onClickStartTimer);
        openUsingGemButton.onClick.AddListener(onClickOpenUsingGem);

    }



    private void onClickStartTimer()
    {

        startTimerButton.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);


        //call start timer function


        openUsingGemButton.gameObject.SetActive(true);
        gemText.gameObject.SetActive(true);


        //set gem Text according to timer.


    }

    private void onClickOpenUsingGem()
    {
        //deduct gemCostToOpen from the total no of remaining gem

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
