using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GenerateChest : MonoBehaviour
{

    private Button generateButton;

    [SerializeField] private ChestScriptableObject[] chestPool;  

    void Start()
    {
        generateButton = GetComponent<Button>();
        generateButton.onClick.AddListener(GenerateChestOnClick);
    }

    private void GenerateChestOnClick()
    {
        //pick a random chest from the chest pool
        
        //and populate it in the chest slot(max 4)

        //check if currently chest queue has no more then 3 chest

        //if full show popUp saying chest full
    }
}
