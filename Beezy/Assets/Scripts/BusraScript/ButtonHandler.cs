using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Sprite[] animImgs1;
    public Image animateImgObj1;

    public Sprite[] animImgs2;
    public Image animateImgObj2;

    public Sprite[] animImgs3;
    public Image animateImgObj3;

    public float animationSpeed = 2f;

    public void Button1()
    {
        animateImgObj1.sprite = animImgs1[(int)(Time.time * animationSpeed * 10) % animImgs1.Length];
    }

    public void Button2()
    {
        animateImgObj2.sprite = animImgs2[(int)(Time.time * animationSpeed * 10) % animImgs2.Length];
    }

    public void Button3()
    {
        animateImgObj3.sprite = animImgs3[(int)(Time.time * animationSpeed * 10) % animImgs3.Length];
    }
}
