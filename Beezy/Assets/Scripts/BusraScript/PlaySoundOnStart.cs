using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    
    private void Awake()
    {
        
        SoundManager.Instance.ChangeMasterVolume(0.5f); // �stedi�iniz ayarlara g�re volume de�erini ayarlayabilirsiniz
    }


}
