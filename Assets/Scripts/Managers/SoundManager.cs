using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    AudioSource localAudio;

    override public void Init()
    {
        base.Init();
        localAudio = this.gameObject.AddComponent<AudioSource>();
        foreach(AudioClip clip in Resources.LoadAll<AudioClip>("Sounds"))
        {
            clips.Add(clip.name, clip);
        }
    }

    public void PlayOneShot(string clipName)
    {
        localAudio.PlayOneShot(clips[clipName]);
    }

    public void SetBackGroundMusic(string clipName)
    {
        localAudio.Stop();
        localAudio.clip = clips[clipName];
        localAudio.Play();
    }
}
