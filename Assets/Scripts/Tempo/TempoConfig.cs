using System.Collections.Generic;
using UnityEngine;

public class TempoConfig : MonoBehaviour
{
    public bool useSubdivisions;
    [HideInInspector]
    public List<TimingOption> timingOptions;

    public TempoConfig()
    {
        if (timingOptions == null)
        {
            timingOptions = new List<TimingOption>();
        }
    }

    // might not need to have the file saving if we can save it to the component
    // then we can drag and drop configurations onto tempos
}
