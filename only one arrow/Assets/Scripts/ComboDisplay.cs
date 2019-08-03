using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ComboDisplay : MonoBehaviour
{
    [SerializeField]
    Color defaultColor;
    [SerializeField]
    Color abilityReadyColor;

    TextMeshProUGUI text;

    public void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
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
        text.text = $"Combo x{player.ComboCount}";
        text.color = player.AbilityReady ? abilityReadyColor : defaultColor;
    }
}