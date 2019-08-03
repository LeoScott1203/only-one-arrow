using UnityEngine;

public class PerkData : ScriptableObject
{
    [SerializeField]
    Perk perk;

    [SerializeField]
    PerkCategory category;

    [SerializeField]
    string description; // No separate name needed

    [SerializeField]
    Sprite icon;
}

public enum PerkCategory
{
    GenericMovement,
    GenericAttack,
    SpecialDash,
    SpecialAttack
}