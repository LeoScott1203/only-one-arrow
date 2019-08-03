﻿using System.Collections;

public static class Perks
{
    static BitArray unlockedPerks = new BitArray((int)Perk.Count);

    public static void Unlock(Perk perk)
    {
        unlockedPerks[(int)perk] = true;
    }

    public static bool IsUnlocked(Perk perk)
    {
        return unlockedPerks[(int)perk];
    }
}

public enum Perk
{
    // Always add new perks before Count so that it stays up to date in a proper way - Urist
    ArrowStartingSpeedPerDrawUnit,
    Count
}