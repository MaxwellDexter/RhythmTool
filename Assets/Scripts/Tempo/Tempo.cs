using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tempo class for starting and stopping tempo. Keeps the beat.
/// </summary>
public class Tempo : MonoBehaviour
{
    private bool hasStarted;
    private double secsPerBeat;
    private double startTime;
    private double currentBeatTime;
    private double nextBeatTime;
    private BeatInformer informer;
    private double latencyMilliseconds;

    [HideInInspector]
    public List<TimingOption> timingOptions;

    public double CurrentBeatTime
    {
        get { return currentBeatTime; }
    }

    private void Start()
    {
        informer = BeatInformer.GetInstance();
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
            // also grab config data and store it
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
            return null;
        }
        else return GetTimingOption(time, currentBeatTime, nextBeatTime);
    }

    private TimingOption GetTimingOption(double theTime, double currentBeat, double nextBeat)
    {
        double latency = TempoUtils.GetSecondsFromMilliseconds(latencyMilliseconds);
        double timeMinusLatency = theTime - latency;
        double beatToCompareTo = currentBeat;

        // if it's past the halfway point of the beat
        if (timeMinusLatency >= currentBeat + (secsPerBeat / 2))
        {
            Debug.Log("Before!");
            beatToCompareTo = currentBeat + secsPerBeat;
        }

        foreach (TimingOption option in timingOptions)
        {
            if (IsInWindow(timeMinusLatency, beatToCompareTo, option.window * secsPerBeat, option.offsetFromBeat * secsPerBeat, false))
            {
                return option;
            }
        }

        throw new System.Exception("Timing Option was not found for beat! Please ensure that your timing options cover the entire beat!");
    }

    private bool IsInWindow(double inputTime, double beatTime, double window, double offset, bool multiDirectional)
    {
        if (multiDirectional)
        {
            return inputTime >= beatTime + offset - window && inputTime < beatTime + offset + window;
        }
        else
        {
            return inputTime >= beatTime + offset && inputTime < beatTime + offset + window;
        }
    }
}
