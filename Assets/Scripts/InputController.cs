using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject tempo;
    private TempoManager man;
    public Sound tapSound;
    private TempoStartInformer informer;
    private FlyingTextShower textShower;

    private void Start()
    {
        man = tempo.GetComponent<TempoManager>();
        tapSound = SoundUtils.MakeSource(tapSound, gameObject.AddComponent<AudioSource>());
        informer = TempoStartInformer.GetInstance();
        textShower = GetComponent<FlyingTextShower>();
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
            TimingOption result = man.IsInTime(AudioSettings.dspTime);
            textShower.SetColor(result.associatedColor);
            textShower.ShowText(result.name, new Vector2(0, 1), false);
        }
    }
}
