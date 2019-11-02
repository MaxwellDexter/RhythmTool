using System.Collections.Generic;
using UnityEngine;

public class TempoConfig : MonoBehaviour
{
    public bool useSubdivisions;

    [HideInInspector]
    public int subdivisionsPerBeat;

    [HideInInspector]
    public bool useSwing;

    [HideInInspector]
    public float swingAmount;

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
