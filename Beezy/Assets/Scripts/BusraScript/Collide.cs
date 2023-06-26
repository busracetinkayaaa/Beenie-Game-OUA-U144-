using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collide : MonoBehaviour
{

    public int maxCan =30; // Maksimum can deðeri
    public int currentCan; // Mevcut can deðeri

    public Slider canBarSlider; // Can barý UI elemaný
                                //  public RectTransform canBarFill; // Can barýnýn doluluðunu gösteren UI elemaný

   // public Animator canAnimasyon;

    void Start()
    {
     
        currentCan = maxCan; // Karakterin baþlangýç caný
        canBarSlider.maxValue = maxCan; // Slider'ýn maksimum deðerini ayarla
        canBarSlider.value = currentCan;
    }

    void Awake()
    {
        canBarSlider = GetComponentInChildren<Slider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
        if (other.CompareTag("wasp"))
        {
            Debug.Log("2");

            Enemies wasp = other.GetComponent<Enemies>();

            if (wasp != null)
            {
                wasp.Attack(this);
            }
        }
    }
    public void HasarAl(int hasarMiktarý)
    {
        currentCan -= hasarMiktarý; // Hasar miktarýný can deðerinden çýkar

       // canAnimasyon.Play("CanAzalmaAnimasyonu");


        if (currentCan <= 0)
        {
            currentCan = 0; // Can deðeri sýfýra düþerse
            // Ölme iþlemleri veya diðer senaryolar burada gerçekleþtirilebilir.
            // Time.timeScale = 0; // Oyunu duraklat
            // karakterin öldüðü animasyon eklenir.
            // SceneManager.LoadScene("GameOverScene");
        }

        // Can barýnýn doluluðunu güncelle
        canBarSlider.value = currentCan;


    }
}



 