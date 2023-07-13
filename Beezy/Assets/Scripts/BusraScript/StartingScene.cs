using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingScene : MonoBehaviour
{

    public Sprite[] animImgs;
    public Image animateImgObj;
    public float animationSpeed = 0.5f;


    // Start is called before the first frame update

    private void Start()
    {
     
    }


    // Update is called once per frame
    void Update()
    {

        animateImgObj.sprite = animImgs[(int)(Time.time * animationSpeed * 10) % animImgs.Length];

    }
}