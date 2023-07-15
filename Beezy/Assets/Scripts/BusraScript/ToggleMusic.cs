using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMusic : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic ;
    [SerializeField] private bool _toggleEffects ;

    public void Toggle()
    {
        if (_toggleEffects) SoundManager.Instance.ToggleEffects();
        if (_toggleMusic) SoundManager.Instance.ToggleMusic();

    }


}
