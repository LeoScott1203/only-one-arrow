using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChargeLevelVisualiser : MonoBehaviour
{
    [SerializeField]
    GameObject chargeLevelProviderSource; // Sadly, you cannot serialise interface fields, so I have to do this workaround; with more time, it'd be more elegant

    [SerializeField]
    Color colorAtMin;
    [SerializeField]
    Color colorAtMax;
    
    IChargeLevelProvider chargeLevelProvider;
    new SpriteRenderer renderer;

    public void Awake()
    {
        chargeLevelProvider = chargeLevelProviderSource.GetComponent<IChargeLevelProvider>();

        if(chargeLevelProvider == null)
        {
            Debug.LogError($"No IChargeLevelProvider found on {chargeLevelProviderSource} (expected by {gameObject}).");
        }

        renderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        renderer.color = Color.Lerp(colorAtMin, colorAtMax, chargeLevelProvider.ChargeLevel);
    }
}
