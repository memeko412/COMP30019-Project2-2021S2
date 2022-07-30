using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] bgms;
    private Sound currbgm;
    private void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.Output;
        }
        foreach (Sound s in bgms)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.Output;
        }

    }

    public void PlayBGM()
    {
        if((currbgm != null && !currbgm.source.isPlaying) || currbgm == null )
        {
            int random = (int)Random.Range(0.0f, bgms.Length-1) ;
            currbgm = bgms[random];
            currbgm.source.Play();
            //currbgm.source.Stop();
        }
    }


    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if(s.name == name)
            {
                s.source.Play();
                return;
            }
        }
    }
    
}
