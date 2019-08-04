// This exists so that we can tweak values in inspector at runtime without recompiling

using UnityEngine;

public sealed class StatsSingleton : MonoBehaviour
{

    public static StatsSingleton Instance = null;

    [SerializeField]
    float playerSpeedWithoutPerk = 5.0f;
    [SerializeField]
    float playerSpeedWithPerk = 6.25f;
    [SerializeField]
    float playerSpeedMultiplierWithoutArrowWithPerk = 1.5f;

    [SerializeField]
    float arrowStartingDrawUnits = 5.0f;
    [SerializeField]
    float arrowDrawUnitsPerSecondWithoutPerk = 10.0f;
    [SerializeField]
    float arrowDrawUnitsPerSecondWithPerk = 15.0f;
    [SerializeField]
    float arrowMaxDrawUnits = 15.0f;

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

    public static float PlayerSpeedWithoutPerk
    {
        get
        {
            return Instance.playerSpeedWithoutPerk;
        }
    }

    public static float PlayerSpeedWithPerk
    {
        get
        {
            return Instance.playerSpeedWithPerk;
        }
    }

    public static float PlayerSpeedMultiplierWithoutArrowWithPerk
    {
        get
        {
            return Instance.playerSpeedMultiplierWithoutArrowWithPerk;
        }
    }

    public static float ArrowStartingDrawUnits
    {
        get
        {
            return Instance.arrowStartingDrawUnits;
        }
    }

    public static float ArrowDrawUnitsPerSecondWithoutPerk
    {
        get
        {
            return Instance.arrowDrawUnitsPerSecondWithoutPerk;
        }
    }

    public static float ArrowDrawUnitsPerSecondWithPerk
    {
        get
        {
            return Instance.arrowDrawUnitsPerSecondWithPerk;
        }
    }

    public static float ArrowMaxDrawUnits
    {
        get
        {
            return Instance.arrowMaxDrawUnits;
        }
    }

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