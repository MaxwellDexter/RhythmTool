using UnityEngine;

/// <summary>
/// A framework for timing options
/// </summary>
[System.Serializable]
public class TimingOption
{
    /// <summary>
    /// the name of the timing option. has to be unique
    /// </summary>
    public string name;

    /// <summary>
    /// does achieving this timing option break the combo?
    /// </summary>
    public bool breaksCombo;

    /// <summary>
    /// The color that is associated with this timing option. Can be used to apply extra styling to the visualisation.
    /// </summary>
    public Color associatedColor;

    public double window;

    public double offsetFromBeat;
}
