using UnityEngine;

public class TempoManager : MonoBehaviour, ITempoReceiver
{
    private bool tempoFound;
    public TapTempo tapTempo;
    public Tempo tempo;
    public GameObject textObject;

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
        tempo.StartTempo();
        TempoStartInformer.GetInstance().ReceiveTempo(interval);
    }

    public TimingOption IsInTime(double time)
    {
        return tempo.IsInBeat(time);
    }

    public TimingOption IsInTime()
    {
        return tempo.IsInBeat(AudioSettings.dspTime);
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
