public class TempoStartInformer : AbstractInformer<ITempoReceiver>, ITempoReceiver
{
    private double secondsPerBeat;
    private static TempoStartInformer instance;
    
    public static TempoStartInformer GetInstance()
    {
        if (instance == null)
        {
            instance = new TempoStartInformer();
        }
        return instance;
    }

    private TempoStartInformer(){}

    public void ReceiveTempo(double secondsPerBeat)
    {
        this.secondsPerBeat = secondsPerBeat;
        InformReceivers();
    }

    public void StopTempo()
    {
        secondsPerBeat = 0f;
        InformReceivers();
    }

    protected override void InformReceiver(ITempoReceiver receiver)
    {
        if (secondsPerBeat < 0.0001)
        {
            receiver.StopTempo();
        }
        else
        {
            receiver.ReceiveTempo(secondsPerBeat);
        }
    }

}
