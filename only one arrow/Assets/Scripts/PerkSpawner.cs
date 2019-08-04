// Please don't look, there's some awful spaghetti here and I'm ashamed - Urist

using System.Collections.Generic;
using UnityEngine;

public class PerkSpawner : MonoBehaviour
{
    [SerializeField]
    List<PerkSpawnPod> tripleSpawnPoints; // For the generic drops

    [SerializeField]
    List<PerkSpawnPod> doubleSpawnPoints; // For the special drops

    [SerializeField]
    List<RewardCategory> rewardsPerThreshold;

    [SerializeField]
    List<PerkData> genericRewards;

    [SerializeField]
    List<PerkData> dashRewards;

    [SerializeField]
    List<PerkData> attackRewards;
    
    Dictionary<PerkData, bool> genericRewardsAllowedStatus;
    Dictionary<PerkData, bool> dashRewardsAllowedStatus;
    Dictionary<PerkData, bool> attackRewardsAllowedStatus;

    public void Awake()
    {
        genericRewardsAllowedStatus = new Dictionary<PerkData, bool>();
        dashRewardsAllowedStatus = new Dictionary<PerkData, bool>();
        attackRewardsAllowedStatus = new Dictionary<PerkData, bool>();

        foreach(PerkData data in genericRewards)
        {
            genericRewardsAllowedStatus.Add(data, true);
        }

        foreach(PerkData data in dashRewards)
        {
            dashRewardsAllowedStatus.Add(data, true);
        }

        foreach(PerkData data in attackRewards)
        {
            attackRewardsAllowedStatus.Add(data, true);
        }
    }

    public void OnEnable()
    {
        KillCounter.OnKillThresholdReached += OnKillThresholdReached;
        PerkSpawnPod.OnPerkPicked += OnPerkPicked;
        Reset.OnReset += OnReset;
    }

    public void OnDisable()
    {
        KillCounter.OnKillThresholdReached -= OnKillThresholdReached;
        PerkSpawnPod.OnPerkPicked -= OnPerkPicked;
        Reset.OnReset -= OnReset;
    }

    void OnReset()
    {
        List<PerkData> keysToChange = new List<PerkData>(genericRewardsAllowedStatus.Keys);

        foreach(PerkData data in keysToChange)
        {
            genericRewardsAllowedStatus[data] = true;
        }

        keysToChange = new List<PerkData>(dashRewardsAllowedStatus.Keys);

        foreach(PerkData data in keysToChange)
        {
            dashRewardsAllowedStatus[data] = true;
        }

        keysToChange = new List<PerkData>(attackRewardsAllowedStatus.Keys);

        foreach(PerkData data in keysToChange)
        {
            attackRewardsAllowedStatus[data] = true;
        }
    }

    void OnKillThresholdReached(int thresholdIndex)
    {
        if(thresholdIndex < rewardsPerThreshold.Count)
        {
            SpawnRewards(rewardsPerThreshold[thresholdIndex]);
        }
    }

    void SpawnRewards(RewardCategory category)
    {
        // Very naive and dumb approach I think, but I can't be arsed figurign out a better one atm
        switch(category)
        {
            case(RewardCategory.Generic):
            {
                List<PerkData> allowed = new List<PerkData>();

                foreach(KeyValuePair<PerkData, bool> kvp in genericRewardsAllowedStatus)
                {
                    if(kvp.Value)
                    {
                        allowed.Add(kvp.Key);
                    }
                }

                int firstIndex = UnityEngine.Random.Range(0, allowed.Count);
                
                int secondIndex = UnityEngine.Random.Range(0, allowed.Count - 1);
                if(secondIndex >= firstIndex)
                {
                    secondIndex++;
                }

                int thirdIndex = UnityEngine.Random.Range(0, allowed.Count - 2);
                if(thirdIndex >= firstIndex)
                {
                    thirdIndex++;
                }
                if(thirdIndex >= secondIndex)
                {
                    thirdIndex++;
                }
                if(thirdIndex == firstIndex) // Oh _NO_. But I really can't figure out another way, it's late.
                {
                    thirdIndex++;
                }
                
                tripleSpawnPoints[0].LoadData(allowed[firstIndex]);
                tripleSpawnPoints[1].LoadData(allowed[secondIndex]);
                tripleSpawnPoints[2].LoadData(allowed[thirdIndex]);

                break;
            }
            case(RewardCategory.Dash):
            {
                List<PerkData> allowed = new List<PerkData>();

                foreach(KeyValuePair<PerkData, bool> kvp in dashRewardsAllowedStatus)
                {
                    if(kvp.Value)
                    {
                        allowed.Add(kvp.Key);
                    }
                }

                int firstIndex = UnityEngine.Random.Range(0, allowed.Count);
                
                int secondIndex = UnityEngine.Random.Range(0, allowed.Count - 1);
                if(secondIndex >= firstIndex)
                {
                    secondIndex++;
                }
                
                doubleSpawnPoints[0].LoadData(allowed[firstIndex]);
                doubleSpawnPoints[1].LoadData(allowed[secondIndex]);

                break;
            }
            case(RewardCategory.Attack):
            {
                List<PerkData> allowed = new List<PerkData>();

                foreach(KeyValuePair<PerkData, bool> kvp in attackRewardsAllowedStatus)
                {
                    if(kvp.Value)
                    {
                        allowed.Add(kvp.Key);
                    }
                }

                int firstIndex = UnityEngine.Random.Range(0, allowed.Count);
                
                int secondIndex = UnityEngine.Random.Range(0, allowed.Count - 1);
                if(secondIndex >= firstIndex)
                {
                    secondIndex++;
                }
                
                doubleSpawnPoints[0].LoadData(allowed[firstIndex]);
                doubleSpawnPoints[1].LoadData(allowed[secondIndex]);

                break;
            }
        }
    }

    void OnPerkPicked(PerkData data)
    {
        switch(data.Category)
        {
            case(PerkCategory.GenericAttack):
            {
                genericRewardsAllowedStatus[data] = false;
                break;
            }
            case(PerkCategory.GenericMovement):
            {
                genericRewardsAllowedStatus[data] = false;
                break;
            }
            case(PerkCategory.SpecialDash):
            {
                dashRewardsAllowedStatus[data] = false;
                break;
            }
            case(PerkCategory.SpecialAttack):
            {
                attackRewardsAllowedStatus[data] = false;
                break;
            }
        }
    }

    enum RewardCategory
    {
        Generic,
        Dash,
        Attack
    }
}