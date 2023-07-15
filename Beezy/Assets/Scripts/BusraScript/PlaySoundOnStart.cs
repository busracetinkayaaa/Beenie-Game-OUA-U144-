using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    //bunu karaktere eklemeyi unutma
    [SerializeField] private AudioClip _clip;
    void Start()
    {
        Debug.Log("1");
        SoundManager.Instance.PlaySound(_clip);
    }

  
}
