using System;
using System.Collections.Generic;
using UnityEngine;

// I shouldn't mix display and everything else but at this point I'd rather have the game done than not done
[RequireComponent(typeof(TextDisplayInImages))]
public class KillCounter : MonoBehaviour
{
    public static Action<int> OnKillThresholdReached = delegate { };

    [SerializeField]
    List<int> killThresholds;

    TextDisplayInImages textDisplay;

    int killCount = 0;
    int currentKillThreshold = 0;

    public void Awake()
    {
        textDisplay = GetComponent<TextDisplayInImages>();
    }

    public void OnEnable()
    {
        Arrow.OnHit += OnEnemyKill;
    }

    public void OnDisable()
    {
        Arrow.OnHit -= OnEnemyKill;
    }

    void OnEnemyKill(Collider2D collider, ITarget target, Arrow arrow) // Dangit, why pass the collider? It should be on the target or arrow anyway! Oh well
    {
        killCount++;

        if(killCount == killThresholds[currentKillThreshold])
        {
            OnKillThresholdReached(currentKillThreshold);
            currentKillThreshold++;
        }

        textDisplay.DisplayText($"{killCount}/{(currentKillThreshold < killThresholds.Count ? killThresholds[currentKillThreshold] : killThresholds[killThresholds.Count - 1])}"); // To make it not update past the last kill counter
    }
}