using System.Collections;

public static class Perks
{
    static Perks()
    {
        Reset.OnReset += OnReset;
    }

    static BitArray unlockedPerks = new BitArray((int)Perk.Count);

    public static void Unlock(Perk perk)
    {
        unlockedPerks[(int)perk] = true;
    }

    public static bool IsUnlocked(Perk perk)
    {
        return unlockedPerks[(int)perk];
    }

    static void OnReset()
    {
        for(int i = 0; i < unlockedPerks.Count; i++)
        {
            unlockedPerks[i] = false;
        }
    }
}

public enum Perk
{
    // Always add new perks before Count so that it stays up to date in a proper way - Urist
    // Generic movement
    PlayerSpeed, // Implemented
    PlayerSpeedWhenWithoutArrow, // Implemented
    DashDistance, // Implemented
    DashRechargeSpeed, // Implemented
    // Generic attack
    DrawUnitsGainPerSecond, // Implemented
    ArrowStartingSpeedPerDrawUnit, // Implemented
    ArrowFalloffAcceleration, // Implemented
    ArrowBouncebackDispersion, // Implemented
    // Special dash
    StunDash,
    DoubleDash, // Implemented
    MagnetDash,
    TelegibDash,
    // Special attack
    PhantomArrow,
    ExplosiveArrow,
    PiercingArrow,
    TeleportArrow, // Implemented
    Count
}
