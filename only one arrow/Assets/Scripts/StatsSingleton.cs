using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StatsSingleton : MonoBehaviour
{

    public static StatsSingleton Instance = null;

    [SerializeField]
    float playerSpeed = 5.0f;

    [SerializeField]
    float arrowStartingSpeedPerDrawUnitWithoutPerk = 1.5f;
    [SerializeField]
    float arrowStartingSpeedPerDrawUnitWithPerk = 2.0f;

    [SerializeField]
    float arrowFalloffStart = 0.0f;
    [SerializeField]
    float arrowFalloffAccelerationWithoutPerk = 1.0f;
    [SerializeField]
    float arrowFalloffAccelerationWithPerk = 0.6f;
    [SerializeField]
    float arrowFalloffMax = 2.0f;

    [SerializeField]
    float arrowBouncebackAngleDispersionWithoutPerk = 40.0f;
    [SerializeField]
    float arrowBouncebackAngleDispersionWithPerk = 30.0f;

    public static float ArrowStartingSpeedPerDrawUnitWithoutPerk
    {
        get
        {
            return Instance.arrowStartingSpeedPerDrawUnitWithoutPerk;
        }
    }

    public static float ArrowStartingSpeedPerDrawUnitWithPerk
    {
        get
        {
            return Instance.arrowStartingSpeedPerDrawUnitWithPerk;
        }
    }

    public static float PlayerSpeed
    {
        get
        {
            return Instance.playerSpeed;
        }
    }

    public static float ArrowFalloffStart
    {
        get
        {
            return Instance.arrowFalloffStart;
        }
    }

    public static float ArrowFalloffAccelerationWithoutPerk
    {
        get
        {
            return Instance.arrowFalloffAccelerationWithoutPerk;
        }
    }

    public static float ArrowFalloffAccelerationWithPerk
    {
        get
        {
            return Instance.arrowFalloffAccelerationWithPerk;
        }
    }

    public static float ArrowFalloffMax
    {
        get
        {
            return Instance.arrowFalloffMax;
        }
    }

    public static float ArrowBouncebackAngleDispersionWithoutPerk
    {
        get
        {
            return Instance.arrowBouncebackAngleDispersionWithoutPerk;
        }
    }
    
    public static float ArrowBouncebackAngleDispersionWithPerk
    {
        get
        {
            return Instance.arrowBouncebackAngleDispersionWithPerk;
        }
    }

    void Awake()
    {
        Instance = this;
    }
}