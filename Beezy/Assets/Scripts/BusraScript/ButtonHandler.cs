using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Sprite[] animImgs;
    public Image animateImgObj;
    public float animationSpeed = 2f;




    public void Button()
    {
        animateImgObj.sprite = animImgs[(int)(Time.time * animationSpeed * 10) % animImgs.Length];

    }


}
