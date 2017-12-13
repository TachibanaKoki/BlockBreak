﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips
{
    private List<AudioClip> _audioClips;
    private List<string> _IDs;

    
    public AudioClips()
    {
        _audioClips = new List<AudioClip>();
        _IDs = new List<string>();
    }

    public AudioClips(AudioClip[] clips)
    {
        _audioClips = new List<UnityEngine.AudioClip>();
        _IDs = new List<string>();
        AddClips(clips);

    }

    public AudioClip GetClip(string id)
    {
        int index =_IDs.FindIndex(x=>x==id);

        return _audioClips[index];
    }

    public void AddClips(AudioClip[] clips)
    {
        _audioClips.AddRange(clips);
        foreach (var clip in clips)
        {
            _IDs.Add(clip.name);
        }
        Debug.Log("AudioClipを登録しました");
    }

    public void RemoveClip(AudioClip clip)
    {
        if (!_IDs.Remove(clip.name))
        {
            Debug.LogWarning("AudioClip:"+clip.name+"が登録されていません");
            return;
        }
        if (_audioClips.Remove(clip))
        {
            Debug.Log("audio clip:" + clip.name+"を登録から削除しました");
            
        }
    }

    public void RemoveClips(AudioClip[] clips)
    {
        foreach(var clip in clips)
        {
            RemoveClip(clip);
        }
    }

    public void Clear()
    {
        _audioClips.Clear();
        _IDs.Clear();
    }


}


[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    static AudioSource _audioSource;

    static AudioClips SE= new AudioClips();

    static AudioClips BGM = new AudioClips();

    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        GameObject go = new GameObject();
        go.AddComponent<SoundManager>();
    }

    void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
            //Resourcesから一括キャッシュ
            SE.AddClips(Resources.LoadAll<AudioClip>("SE"));
            BGM.AddClips(Resources.LoadAll<AudioClip>("BGM"));
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }



    static void PlayBGM(string soundId)
    {
        _audioSource.clip = BGM.GetClip(soundId);
        _audioSource.Play();
    }

    static void PlaySE(string soundId)
    {
        AudioClip clip = SE.GetClip(soundId);
        _audioSource.PlayOneShot(clip);
    }
}
