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
        foreach (AudioClip clip in Resources.LoadAll<AudioClip>("Sounds"))
        {
            clips.Add(clip.name, clip);
        }
    }

    /// <summary>
    /// 전역에서 재생되어야 하는 효과음을 재생합니다.
    /// </summary>
    /// <param name="clipName"></param>
    public void PlayOneShot(string clipName)
    {
        localAudio.PlayOneShot(clips[clipName]);
    }

    /// <summary>
    /// 특정 지점에서 재생되어야 하는 효과음을 재생합니다, 위치값이 필요합니다.
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="pos"></param>
    public void PlayOneShotPoint(string clipName, Vector3 pos)
    {
        if (IsEffectOn() == false)
            return;

        AudioSource.PlayClipAtPoint(clips[clipName], pos, 0.2f);
    }

    /// <summary>
    /// BGM을 설정합니다.
    /// </summary>
    /// <param name="clipName"></param>
    public void SetBackGroundMusic(string clipName, bool isLoop = true)
    {
        if (IsBGMOn() == false)
            return;

        localAudio.Stop();
        localAudio.clip = clips[clipName];
        localAudio.loop = isLoop;
        localAudio.volume = 0.2f;
        localAudio.Play();
    }

    public void ToggleBGM(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("PlayBGM", 1);
            localAudio.Play();
        }
        else
        {
            PlayerPrefs.SetInt("PlayBGM", 0);
            localAudio.Stop();
        }
    }

    public void ToggleEffect(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("PlayEffect", 1);
        }
        else
        {
            PlayerPrefs.SetInt("PlayEffect", 0);
        }
    }
    
    public bool IsBGMOn()
    {
        return PlayerPrefs.GetInt("PlayBGM", 1) == 1;
    }
    
    public bool IsEffectOn()
    {
        return PlayerPrefs.GetInt("PlayEffect", 1) == 1;
    }
}