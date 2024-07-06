using UnityEngine;

[CreateAssetMenu(fileName = "New TowerData", menuName = "Data/TowerData", order = 1)]
public class TowerData : ScriptableObject
{
    public GameObject prefab;

    public Sprite btnIcon;
    public string description;
    public bool canBuy;

    public float damage;
    public float rateOfFire;
    public float range;

    public int placeCost;
    public int baseUpgradeCost;
    public int baseLevelUpCost;

    public string category;
    public string[] damageType;

}
