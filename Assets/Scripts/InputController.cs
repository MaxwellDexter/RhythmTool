using UnityEngine;

public class InputController : AbstractTempoReceiver
{
    public GameObject tempo;
    private TempoManager man;
    public Sound tapSound;
    TempoStartInformer informer;

    private void Start()
    {
        base.Start();
        man = tempo.GetComponent<TempoManager>();
        tapSound = SoundUtils.MakeSource(tapSound, gameObject.AddComponent<AudioSource>());
        informer = TempoStartInformer.GetInstance();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            man.Tap();
            tapSound.source.Play();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            informer.StopTempo();
            // play vinyl stop se?
        }
        else if (Input.anyKeyDown)
        {
            Debug.Log(man.IsInTime(AudioSettings.dspTime).ToString());
        }
    }

    public override void OnBeat()
    {
        //Debug.Log(man.IsInTime(AudioSettings.dspTime).ToString());
    }
}
