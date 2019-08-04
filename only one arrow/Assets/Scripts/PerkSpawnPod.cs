using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider2D))]
public class PerkSpawnPod : MonoBehaviour
{
    [SerializeField]
    new Collider2D collider;

    [SerializeField]
    SpriteRenderer iconDisplay;

    [SerializeField]
    TextMeshPro descriptionDisplay;

    public static Action<PerkData> OnPerkPicked = delegate { };

    PerkData data = null;

    public void Awake()
    {
        Unload();
    }

    public void OnEnable()
    {
        OnPerkPicked += OnOtherPerkPicked;
        Reset.OnReset += OnReset;
    }

    public void OnDisable()
    {
        OnPerkPicked -= OnOtherPerkPicked;
        Reset.OnReset -= OnReset;
    }

    void OnReset()
    {
        Unload();
    }

    public void LoadData(PerkData data)
    {
        collider.enabled = true;
        iconDisplay.enabled = true;
        descriptionDisplay.enabled = true;

        iconDisplay.sprite = data.Icon;
        descriptionDisplay.text = data.Description;

        this.data = data;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Arrow arrow = other.GetComponent<Arrow>(); // I guess shooting works

        if(arrow != null)
        {
            Perks.Unlock(data.Perk);

            OnPerkPicked(data);
        }
    }

    void OnOtherPerkPicked(PerkData data)
    {
        Unload();
    }

    void Unload()
    {
        collider.enabled = false;
        iconDisplay.enabled = false;
        descriptionDisplay.enabled = false;

        this.data = null;
    }
}