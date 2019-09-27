using UnityEngine;

[System.Serializable]
public class MusicClip
{
    public double bpm;

    public AudioClip clip;

    public BeatType type;

    public Subdivision subdivision;
}
