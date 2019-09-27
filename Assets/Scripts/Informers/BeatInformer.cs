using UnityEngine;

public class BeatInformer : AbstractInformer<IBeatReceiver>, IBeatReceiver
{
    private static BeatInformer instance;

    public static BeatInformer GetInstance()
    {
        if (instance == null)
        {
            instance = new BeatInformer();
        }
        return instance;
    }

    private BeatInformer() : base()
    {
        
    }

    public void OnBeat()
    {
        InformReceivers();
    }

    protected override void InformReceiver(IBeatReceiver receiver)
    {
        receiver.OnBeat();
    }
}
