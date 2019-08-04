// Quick and dirty, not a lot of time left

using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Victory : MonoBehaviour
{
    [SerializeField]
    int indexOfKillThreshold;

    public void OnEnable()
    {
        KillCounter.OnKillThresholdReached += OnKillThresholdReached;
    }

    public void OnDisable()
    {
        KillCounter.OnKillThresholdReached -= OnKillThresholdReached;
    }

    void OnKillThresholdReached(int index)
    {
        if(index == indexOfKillThreshold)
        {
            GetComponent<TextMeshProUGUI>().text = "You win!\n:D :D :D :D :D :D :D"; // Wow underwhelming; also since it's only done once no need to cache
            EnemyMovementBehaviour.ImJustHookingThingsUpToThisBecauseItsFaster(); // please don't do this at home
        }
    }
}