using System.Collections.Generic;
using UnityEngine;

public enum TimingCalculations
{
    Beat,
    Subdivision,
    Pattern,
    Song
}

public class TempoConfig : MonoBehaviour
{
    public TimingCalculations timingCalculation;

    [HideInInspector]
    public int subdivisionsPerBeat;

    [HideInInspector]
    public bool[] pattern;

    [HideInInspector]
    public List<TimingOption> timingOptions;

    public TempoConfig()
    {
        if (timingOptions == null)
        {
            timingOptions = new List<TimingOption>();
        }
    }
}
