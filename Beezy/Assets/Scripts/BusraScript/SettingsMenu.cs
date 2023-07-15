using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image spriteImage;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;


    public void SetFullScreen(bool isFullScreen) 
    {
        Screen.fullScreen = isFullScreen;   
    }

    private void OnToggleValueChanged(bool isOn)
    {
        spriteImage.sprite = isOn ? sprite1 : sprite2;

    }

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

    }


}
