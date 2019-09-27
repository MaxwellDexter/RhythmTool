using System.Collections.Generic;
using UnityEngine;

public class TapTempo : MonoBehaviour
{
    private const float TAP_WINDOW = 5f; // 5 BPM
    private const float SLOWEST_TEMPO = 50f; // 50 bpm

    private double timeAtLastTap;
    private List<double> tapIntervals;
    private TempoManager manager;

    private void Awake()
    {
        ResetTempo();
    }

    public double GetSecondsInterval()
    {
        if (tapIntervals.Count > 0)
        {
            double total = 0f;
            foreach (double i in tapIntervals)
            {
                total += i;
            }
            return total / tapIntervals.Count;
        }
        return 0f;
    }

    private bool IsTappingUniform()
    {
        // .1 each side of the avg
        double avg = GetSecondsInterval();
        bool uniform = true;
        double upperWindow = TempoUtils.FlipBpmInterval(TempoUtils.FlipBpmInterval(avg) + TAP_WINDOW);
        double lowerWindow = TempoUtils.FlipBpmInterval(TempoUtils.FlipBpmInterval(avg) - TAP_WINDOW);
        foreach (double i in tapIntervals)
        {
            uniform = uniform && (i < lowerWindow && i > upperWindow);
        }
        return uniform;
    }

    public void Tap()
    {
        if (!timeAtLastTap.Equals(0f))
        {
            // not first tap
            double interval = AudioSettings.dspTime - timeAtLastTap;
            if (interval > 1)
            {
                // slower than 60 bpm, restarting
                ResetTempo();
            }
            else
            {
                tapIntervals.Add(interval);
            }
        }
        timeAtLastTap = AudioSettings.dspTime;

        if (tapIntervals.Count > 4)
        {
            StartTheTempo();
        }
    }

    private void StartTheTempo()
    {
        manager.ReceiveTempoInMiliseconds(GetSecondsInterval());
        ResetTempo();
    }

    private void ResetTempo()
    {
        timeAtLastTap = 0f;
        tapIntervals = new List<double>();
    }

    public void SetManager(TempoManager man)
    {
        manager = man;
    }
}
