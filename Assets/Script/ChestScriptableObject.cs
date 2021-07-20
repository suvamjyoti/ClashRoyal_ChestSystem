using UnityEngine;

public enum ChestType
{
    Common,Rare,Epic,Legendary
}

public enum ChestState
{
    Locked,Unlocked,NotCollected,Collected
}

[CreateAssetMenu(fileName = "ScriptableChest", menuName = "ScriptableObject/chest")]
public class ChestScriptableObject : ScriptableObject
{
    [SerializeField] public ChestType chestType;
    [SerializeField] public int coinLowerBound;
    [SerializeField] public int coinUpperBound;
    [SerializeField] public int gemsLowerBound;
    [SerializeField] public int gemsUpperBound;
    [SerializeField] public float timer;

    [SerializeField] public Sprite chestSprite;
    [SerializeField] public Animation chestOpenAnimationClip;
}
