using UnityEngine;

/// <summary>
/// A framework for timing options
/// </summary>
[System.Serializable]
public class TimingOption
{
    /// <summary>
    /// Constructor
    /// </summary>
    public TimingOption()
    {
        associatedColor = new Color(255, 255, 255, 255);
        name = "Nice";
    }

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

    /// <summary>
    /// The window in which the timing option is active.
    /// <br></br>
    /// Will be multiplied by the seconds per beat to get the actual seconds this is active for.
    /// <br></br>
    /// Has to be a percentage in 0-1 format.
    /// <br></br>
    /// When making windows with offsets be mindful that you can only have a range of -0.5 to 0.5. This covers the full range of a beat from halfway before it happens to halfway after it happens.
    /// <br></br>
    /// E.G. 0.59 for 59% or 0.1 for 10%
    /// </summary>
    public double window;

    /// <summary>
    /// The offset from the beat to put the active window in.
    /// <br></br>
    /// Must be in percentage 0-1 format. Can (should) have negative values for windows before the beat (early).
    /// 
    /// <br></br>
    /// When making windows with offsets be mindful that you can only have a range of -0.5 to 0.5.
    /// This covers the full range of a beat from halfway before it happens to halfway after it happens.
    /// <br></br>
    /// If there was a window of 0.2 and an offset of 0.3, the timing option would be active between the time equivalent of 0.3 and 0.5
    /// </summary>
    public double offsetFromBeat;
}
