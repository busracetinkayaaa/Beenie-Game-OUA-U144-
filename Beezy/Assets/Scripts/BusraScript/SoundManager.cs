using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider audioSlider;

    private void Start()
    {
        audioSlider.value = audioSource.volume;
        audioSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    public void OnSliderChanged(float Volume)
    {
        audioSource.volume = Volume;
    }

    public void SesKapat()
    {
        audioSource.volume = 0f;
    }
}
 
  
  