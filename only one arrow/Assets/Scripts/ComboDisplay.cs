using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextDisplayInImages))]
public class ComboDisplay : MonoBehaviour
{
    [SerializeField]
    Color defaultColor;
    [SerializeField]
    Color abilityReadyColor;

    TextDisplayInImages text;

    public void Awake()
    {
        text = GetComponent<TextDisplayInImages>();
    }

    public void OnEnable()
    {
        Player.OnComboChange += OnComboChange;
    }

    public void OnDisable()
    {
        Player.OnComboChange -= OnComboChange;
    }

    void OnComboChange(Player player)
    {
        text.DisplayText($"x{player.ComboCount}");
        text.SetColor(player.AbilityReady ? abilityReadyColor : defaultColor);
    }
}