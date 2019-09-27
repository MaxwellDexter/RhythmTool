using System;
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

    public double CurrentBeatTime
    {
        get { return currentBeatTime; }
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
            if (currentTime > currentBeatTime)
            {
                currentBeatTime += secsPerBeat;
                // trigger beat
                Debug.Log("beat");
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
        if (IsInWindow(currentBeatTime, time, windowModifier))
        {
            return TimingOption.Good;
        }
        return TimingOption.Early;
    }

    private bool IsInWindow(double baseTime, double newTime, float window)
    {
        return newTime > baseTime - window && newTime < baseTime + window;
    }
}
