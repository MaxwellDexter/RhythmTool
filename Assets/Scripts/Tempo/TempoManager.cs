using UnityEngine;

public class TempoManager : MonoBehaviour, ITempoReceiver
{
    private bool tempoFound;
    public bool useTapTempo;
    public TapTempo tapTempo;
    public Tempo tempo;

    public TempoManager()
    {
        tempoFound = false;
    }

    private void Start()
    {
        tapTempo.SetManager(this);
        TempoStartInformer.GetInstance().RegisterReceiver(this);
    }

    public void ReceiveTempoInMiliseconds(double interval)
    {
        tempoFound = true;
        tempo.SetTempo(interval);
        tempo.SetLatencyMilliseconds(50);
        tempo.StartTempo();
        Debug.Log("BPM: " + TempoUtils.FlipBpmInterval(interval));
        TempoStartInformer.GetInstance().ReceiveTempo(interval);
	}

	public TimingOption IsInTime(double time)
    {
        return tempo.IsInBeat(time);
    }

    public TimingOption IsInTime()
    {
        return tempo.IsInBeat();
    }

    public void Tap()
    {
        if (!tempoFound)
        {
            tapTempo.Tap();
        }
    }

    public double GetCurrentBeatTime()
    {
        return tempo.CurrentBeatTime;
    }

    public void ReceiveTempo(double secondsPerBeat)
    {
        // nothing
    }

    public void StopTempo()
    {
        tempo.StopTempo();
        tempoFound = false;
    }
}
