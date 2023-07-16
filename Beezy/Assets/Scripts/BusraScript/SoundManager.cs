using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource,_effectsSource;

    public static SoundManager Instance;


    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else 
        {
            Destroy(gameObject);
        }
    }
    /*
    public void PlaySound(AudioClip clip) 
    {
        _effectsSource.PlayOneShot(clip);
    }*/

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;

    }

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;

    }
}
 
  
  