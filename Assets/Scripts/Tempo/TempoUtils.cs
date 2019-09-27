using System.Collections.Generic;

public static class TempoUtils
{
    private static readonly int GLOBAL_SUBDIVISION_MULTIPLIER = 4;

    public static double FlipBpmInterval(double toFlip)
    {
        return 60.0 / toFlip;
    }

    public static double GetMillisecondsFromSeconds(double seconds)
    {
        return seconds * 1000;
    }

    public static double GetSecondsFromMilliseconds(double milliseconds)
    {
        return milliseconds / 1000;
    }

    public static double GetPitchModifierFromBpm(double newBpm, double referenceBpm)
    {
        return newBpm / referenceBpm;
    }
}
