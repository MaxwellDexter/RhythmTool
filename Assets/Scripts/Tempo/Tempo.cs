using UnityEngine;

/// <summary>
/// Tempo class for starting and stopping tempo. Keeps the beat.
/// </summary>
public class Tempo : MonoBehaviour
{
    public float windowModifier;

    private bool hasStarted;
    private double secsPerBeat;
    private double startTime;
    private double currentBeatTime;
    private double nextBeatTime;
    private BeatInformer informer;
    private double latencyMilliseconds;

    public Sound tempoSound;

    public double CurrentBeatTime
    {
        get { return currentBeatTime; }
    }

    private void Start()
    {
        informer = BeatInformer.GetInstance();
        tempoSound = SoundUtils.MakeSource(tempoSound, gameObject.AddComponent<AudioSource>());
    }

    /// <summary>
    /// Sets the tempo. Please call StartTempo() afterwards to start it.
    /// </summary>
    /// <param name="secondInterval">the interval between each beat in seconds</param>
    public void SetTempo(double secondInterval)
    {
        secsPerBeat = secondInterval;
    }

    /// <summary>
    /// Sets the latency of the tempo in milliseconds. For calculating the timing options.
    /// </summary>
    /// <param name="theLatency">in milliseconds</param>
    public void SetLatency(double theLatency)
    {
        latencyMilliseconds = theLatency;
    }

    /// <summary>
    /// Starts the tempo. Will throw an exception if you haven't called SetTempo() yet.
    /// </summary>
    public void StartTempo()
    {
        if (secsPerBeat == 0)
        {
            throw new System.Exception("You can't start the tempo without a tempo! Please set the tempo prior to starting!");
        }
        else
        {
            startTime = AudioSettings.dspTime;
            currentBeatTime = startTime;
            hasStarted = true;
            // need trigger to music player
        }
    }

    public void StopTempo()
    {
        hasStarted = false;
        startTime = 0;
        currentBeatTime = 0;
    }

    private void Update()
    {
        if (hasStarted)
        {
            double currentTime = AudioSettings.dspTime;
            bool hasGonePastBeat = false;
            while (currentTime > currentBeatTime + secsPerBeat)
            {
                hasGonePastBeat = true;
                currentBeatTime += secsPerBeat;
                nextBeatTime = currentBeatTime + secsPerBeat;
            }
            if (hasGonePastBeat)
            {
                informer.OnBeat();
                //tempoSound.source.Play();
                // still need to think about early for next beat
            }
        }
    }

    /// <summary>
    /// Is the time you have in time with the beat? Passes in the current time retrieved from AudioSettings.
    /// </summary>
    /// <returns>The timing option for the beat</returns>
    public TimingOption IsInBeat()
    {
        return IsInBeat(AudioSettings.dspTime);
    }

    /// <summary>
    /// Is the time you have in time with the beat?
    /// </summary>
    /// <param name="time">Your time that you have calculated when the user presses a button or something.</param>
    /// <returns>The timing option for the beat</returns>
    public TimingOption IsInBeat(double time)
    {
        if (!hasStarted)
        {
            return TimingOption.Bad;
        }
        else return GetTimingOption(time, currentBeatTime, nextBeatTime);
    }

    private TimingOption GetTimingOption(double theTime, double currentBeat, double nextBeat)
    {
        double latency = TempoUtils.GetSecondsFromMilliseconds(latencyMilliseconds);
        double timeMinusLatency = theTime - latency;
        double beatToCompareTo = currentBeat;

        if (timeMinusLatency < currentBeat)
        {
            beatToCompareTo = currentBeat - secsPerBeat;
            Debug.Log("could've been bad!");
        }

        // perfect
        if (IsInWindow(timeMinusLatency, beatToCompareTo, PercentToSeconds(secsPerBeat, 6.25), 0, false))
        {
            return TimingOption.Perfect;
        }
        // good
        if (IsInWindow(timeMinusLatency, beatToCompareTo, PercentToSeconds(secsPerBeat, 6.25), PercentToSeconds(secsPerBeat, 12.5), true))
        {
            return TimingOption.Good;
        }
        // late
        if (IsInWindow(timeMinusLatency, beatToCompareTo, PercentToSeconds(secsPerBeat, 6.25), PercentToSeconds(secsPerBeat, 25), true))
        {
            return TimingOption.Late;
        }

        // bad
        if (IsInWindow(timeMinusLatency, beatToCompareTo, PercentToSeconds(secsPerBeat, 18.75), PercentToSeconds(secsPerBeat, 50), true))
        {
            return TimingOption.Bad;
        }


        // next beat

        // early
        if (IsInWindow(timeMinusLatency, beatToCompareTo, PercentToSeconds(secsPerBeat, 6.25), PercentToSeconds(secsPerBeat, 75), true))
        {
            return TimingOption.Early;
        }
        // good
        if (IsInWindow(timeMinusLatency, beatToCompareTo, PercentToSeconds(secsPerBeat, 6.25), PercentToSeconds(secsPerBeat, 87.5), true))
        {
            Debug.Log("early good");
            return TimingOption.Good;
        }
        // perfect
        if (IsInWindow(timeMinusLatency, beatToCompareTo, PercentToSeconds(secsPerBeat, 6.25), PercentToSeconds(secsPerBeat, 93.75), false))
        {
            Debug.Log("early perfect");
            return TimingOption.Perfect;
        }

        Debug.Log("END BAD!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        Debug.Log("Current: " + beatToCompareTo + " This hit: " + timeMinusLatency);
        return TimingOption.Bad; // log something instead
    }

    private double PercentToSeconds(double secondsPerBeat, double percentage)
    {
        return secondsPerBeat * (percentage * 0.01);
    }

    private bool IsInWindow(double theTime, double initialTime, double window, double offset, bool multiDirectional)
    {
        if (multiDirectional)
        {
            return theTime > initialTime + offset - window && theTime < initialTime + offset + window;
        }
        else
        {
            return theTime > initialTime + offset && theTime < initialTime + offset + window;
        }
    }
}
