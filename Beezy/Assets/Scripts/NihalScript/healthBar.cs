using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public static healthBar instance;
    private int currentHealth;

    public Sprite[] healthImgs;
    public Image animateHealth;

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

    public void SetHealth(int value)
    {
        currentHealth = value;
        Debug.Log("currentHealth"+ currentHealth);
        UpdateHealthBar();
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    private void UpdateHealthBar()
    {
        int index = Mathf.Clamp(currentHealth / 20, 0, healthImgs.Length - 1);
        animateHealth.sprite = healthImgs[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        int defaultHealth = 100;
        SetHealth(defaultHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
