using UnityEngine;

public class SoundUtils
{
    public static Sound MakeSource(Sound s, AudioSource source)
    {
        s.source = source;
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = s.spatialBlend;
        s.source.maxDistance = s.maxDistance;
        s.source.rolloffMode = s.rollOffMode;
        return s;
    }
}
