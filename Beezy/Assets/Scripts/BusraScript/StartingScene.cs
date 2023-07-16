using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingScene : MonoBehaviour
{

    public Sprite[] animImgs;
    public Image animateImgObj;
    public float animationSpeed = 0.5f;

    private void Awake()
    {
        SoundManager.Instance.ChangeMasterVolume(0.5f);
    }
    void Update()
    {

        animateImgObj.sprite = animImgs[(int)(Time.time * animationSpeed * 10) % animImgs.Length];

    }
}