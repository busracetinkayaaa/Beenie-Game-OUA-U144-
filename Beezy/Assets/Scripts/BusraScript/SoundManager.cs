using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource,_effectsSource;

    public static SoundManager Instance;

    [SerializeField] public Button button;
    [SerializeField] public Sprite pressedSprite;
    [SerializeField] public Sprite defaultSprite;

    private bool isPressed = false;


    private void Awake()
    {
        if (Instance == null) 
        {
            Debug.Log("instance");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else 
        {
            Debug.Log("deneme");
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip) 
    {
        _effectsSource.PlayOneShot(clip);
    }

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

        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            if (isPressed)
            {
                buttonImage.sprite = defaultSprite;
            }
            else
            {
                buttonImage.sprite = pressedSprite;
            }

            isPressed = !isPressed;
        }
    }
}
 
  
  