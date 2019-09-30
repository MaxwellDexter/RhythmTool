using UnityEngine;
using TMPro;

public class BPMDisplay : MonoBehaviour, ITempoReceiver
{
    private readonly string BPM_DISPLAY = "BPM: ";
    private readonly string UNKNOWN = "unknown";
    private double secondsPerBeat;
    private bool shouldUpdateText;
    private TextMeshProUGUI text;

    private void Start()
    {
        TempoStartInformer.GetInstance().RegisterReceiver(this);
        secondsPerBeat = -1;
        shouldUpdateText = true;
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (shouldUpdateText)
        {
            UpdateText(secondsPerBeat < 0);
            shouldUpdateText = false;
        }
    }

    public void ReceiveTempo(double secondsPerBeat)
    {
        this.secondsPerBeat = secondsPerBeat;
        shouldUpdateText = true;
    }

    public void StopTempo()
    {
        secondsPerBeat = -1;
        shouldUpdateText = true;
    }

    private void UpdateText(bool hasBpm)
    {
        text.text = BPM_DISPLAY + (hasBpm ? UNKNOWN :
            System.Math.Round(TempoUtils.FlipBpmInterval(secondsPerBeat), 1).ToString());
    }
}
