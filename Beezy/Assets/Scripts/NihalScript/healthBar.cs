using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public static healthBar instance;
    public static int beeHealth = 100;

    public Sprite[] beeHealthImgs;
    public Image animateHealth;

    public void UpdateHealth(int value)
    {
        beeHealth = value;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
