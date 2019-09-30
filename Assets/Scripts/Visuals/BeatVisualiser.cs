using UnityEngine;

public class BeatVisualiser : AbstractTempoReceiver
{
    public GameObject visualiserOne;
    public GameObject visualiserTwo;

    private bool flipBeat;

    private void Awake()
    {
        visualiserOne.SetActive(true);
        visualiserTwo.SetActive(false);
        flipBeat = false;
    }

    private void Update()
    {
        if (flipBeat)
        {
            visualiserOne.SetActive(!visualiserOne.activeSelf);
            visualiserTwo.SetActive(!visualiserTwo.activeSelf);
            flipBeat = false;
        }
    }

    public override void OnBeat()
    {
        flipBeat = true;
    }

    public void OnSubdivision()
    {

    }
}
