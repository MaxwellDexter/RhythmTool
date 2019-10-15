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
            //TimingEnum result = TimingEnum.Good;
            //textShower.ShowText("yeo", new Vector2(0, 1), false);
            TimingOption result = man.IsInTime(AudioSettings.dspTime);
            //textShower.SetColor(new Color(20, 20, 20));
            textShower.SetColor(result.associatedColor);
            textShower.ShowText(result.name, new Vector2(0, 1), false);
            //Debug.Log();
        }
    }

    private Color GetColor(TimingEnum result)
    {
        switch (result)
        {
            case TimingEnum.Bad:
                return Color.red;

            case TimingEnum.Early:
                return new Color(255, 137, 0);

            case TimingEnum.Good:
                return Color.green;

            case TimingEnum.Perfect:
                return Color.cyan;

            case TimingEnum.Late:
                return new Color(255, 0, 255);
        }
        return Color.white;
    }
}
