using UnityEngine;

public abstract class AbstractTempoReceiver : MonoBehaviour, IBeatReceiver
{
    public abstract void OnBeat();

    private void Start()
    {
        // a licky bom bom dere
        BeatInformer.GetInstance().RegisterReceiver(this);
    }
}
