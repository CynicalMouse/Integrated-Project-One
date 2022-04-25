using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
          
            s.source.outputAudioMixerGroup = s.group;

        }
    }

    void Start()
    {
        Play("MenuTheme");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        return;
        s.source.Play();

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

    }

}
