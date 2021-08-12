using UnityEngine;
using UnityEngine.UI;



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
    [SerializeField] public ChestState chestCurrentState;
}
