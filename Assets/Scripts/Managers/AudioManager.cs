using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Sound> soundslist = new List<Sound>();

    public void Init()
    {
        foreach (Sound s in soundslist)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.spatialBlend = s.spacialBlend;
        }
    }

    public void PlayAudio(string name)
    {
        var audio = soundslist.Find(sound => sound.name == name);

        if (audio == null)
            return;

        audio.source.Play();
    }

    public void PlayLoopedAudio(string name, bool onOrOff)
    {
        var audio = soundslist.Find(sound => sound.name == name);

        if (audio == null)
            return;

        if (onOrOff)
            audio.source.Play();
        else
            audio.source.Stop();
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [HideInInspector] public AudioSource source;

    [Range(0, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [Range(0, 1f)]
    public float spacialBlend;
    public bool loop;
}
