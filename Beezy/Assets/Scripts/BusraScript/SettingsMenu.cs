using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //Toggle
    [SerializeField] private Toggle[] toggles;
 
    [SerializeField] private Image spriteImage;
    [SerializeField] private Sprite[] sprites;

    public void SetVolume(float volume)
    {
      //  audioMixer.SetFloat("volume", volume);

    }

    public void SetFullScreen(bool isFullScreen) 
    {
        Screen.fullScreen = isFullScreen;   
    }

    private void OnToggleValueChanged(int toggleIndex, bool isOn)
    {
        if (isOn && toggleIndex < sprites.Length)
        {
            spriteImage.sprite = sprites[toggleIndex];
        }
    }

    private void Start()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            int toggleIndex = i; // Döngü deðiþkenini yerel deðiþken olarak tut
            toggles[toggleIndex].onValueChanged.AddListener(isOn => OnToggleValueChanged(toggleIndex, isOn));
        }
    }

   
}
