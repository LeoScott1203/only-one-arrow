using UnityEngine;

[CreateAssetMenu(fileName = "PerkData", menuName = "ScriptableObjects/PerkData", order = 1)]
public class PerkData : ScriptableObject
{
    [SerializeField]
    Perk _perk;
    public Perk Perk
    {
        get
        {
            return _perk;
        }
    }

    [SerializeField]
    PerkCategory _category;
    public PerkCategory Category
    {
        get
        {
            return _category;
        }
    }

    [SerializeField]
    string _description; // No separate name needed
    public string Description
    {
        get
        {
            return _description;
        }
    }

    [SerializeField]
    Sprite _icon;
    public Sprite Icon
    {
        get
        {
            return _icon;
        }
    }
}

public enum PerkCategory
{
    GenericMovement,
    GenericAttack,
    SpecialDash,
    SpecialAttack
}