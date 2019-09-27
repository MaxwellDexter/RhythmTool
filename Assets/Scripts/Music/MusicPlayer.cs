using System;
using UnityEngine;

public class MusicPlayer : MonoBehaviour, ITempoReceiver
{
    public Sound currentSong;
    private double secsPerBeat;
    public MusicClip[] clips;
    private TempoManager manager;
    public int BpmPerSplit;

    void Start()
    {
        TempoStartInformer.GetInstance().RegisterReceiver(this);
        manager = GameObject.Find("Tempo").GetComponent<TempoManager>();
    }

    public void ReceiveTempo(double secondsPerBeat)
    {
        secsPerBeat = secondsPerBeat;
        PlayMusic();
    }

    private void PlayMusic()
    {
        // do calculations and set everything up
        double bpm = TempoUtils.FlipBpmInterval(secsPerBeat);
        MusicClip clip = GetClip(bpm);
        currentSong.clip = clip.clip;
        currentSong.pitch = (float)TempoUtils.GetPitchModifierFromBpm(bpm, clip.bpm);
        currentSong = SoundUtils.MakeSource(currentSong, GetAudioSource());

        // check if in beat and play music
        if (manager.IsInTime(AudioSettings.dspTime) == TimingOption.Perfect)
        {
            currentSong.source.Play();
        }
        else
        {
            //currentSong.source.PlayDelayed((float)(AudioSettings.dspTime - (manager.GetCurrentBeatTime() + secsPerBeat)));
            currentSong.source.Play((ulong)(manager.GetCurrentBeatTime() + secsPerBeat));
        }
    }

    private MusicClip GetClip(double bpm)
    {
        foreach (MusicClip clip in clips)
        {
            if (bpm > clip.bpm - BpmPerSplit && bpm < clip.bpm + BpmPerSplit)
            {
                return clip;
            }
        }
        Debug.LogWarning("Could not get the clip! bpm was: " + bpm);
        return clips[clips.Length - 1];
    }

    public void StopTempo()
    {
        try
        {
            currentSong.source.Stop();
        }
        catch (NullReferenceException)
        {
            // don't care
        }
    }

    private AudioSource GetAudioSource()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        if (source == null)
        {
            return gameObject.AddComponent<AudioSource>();
        }
        return source;
    }
}
