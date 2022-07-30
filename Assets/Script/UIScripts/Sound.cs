using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound 
{
    // Start is called before the first frame update
    public string name;
    public AudioClip clip;
    public AudioMixerGroup Output;

    [Range(0f,1f)]
    public float volume;


    public bool loop;
    [HideInInspector]
    public AudioSource source;

}
