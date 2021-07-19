using UnityEngine;

public enum ChestType
{
    Common,Rare,Epic,Legendary
}

[CreateAssetMenu(fileName = "ScriptableChest", menuName = "ScriptableObject/chest")]
public class ChestScriptableObject : ScriptableObject
{
    [SerializeField] ChestType chestType;
    [SerializeField] int coinLowerBound;
    [SerializeField] int coinUpperBound;
    [SerializeField] int gemsLowerBound;
    [SerializeField] int gemsUpperBound;

    [SerializeField] Sprite chestSprite;
    [SerializeField] Animation chestOpenAnimationClip;
}
