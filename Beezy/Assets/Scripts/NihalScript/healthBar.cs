using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public static healthBar instance;
    public static int beeHealth = 100;

    public Sprite[] beeHealthImgs;
    public Image animateBeeHealth;

    public void UpdateHealth(int value)
    {
        beeHealth = value;
        UpdateHealthImage();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    void UpdateHealthImage()
    {
        int index_ = 0;

        if (beeHealth >= 100)
        {
            index_ = 0;
        }
        else if (beeHealth >= 80)
        {
            index_ = 1;
        }
        else if (beeHealth >= 60)
        {
            index_ = 2;
        }
        else if (beeHealth >= 40)
        {
            index_ = 3;
        }
        else if (beeHealth >= 20)
        {
            index_ = 4;
        }

        else if (beeHealth == 0)
        {
            index_ = 5;
        }

        animateBeeHealth.sprite = beeHealthImgs[index_];

        Debug.Log("Health: " + beeHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
