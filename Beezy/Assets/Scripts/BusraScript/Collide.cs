using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collide : MonoBehaviour
{

    public int maxCan =30; // Maksimum can değeri
    public int currentCan; // Mevcut can değeri

    public Slider canBarSlider; // Can barı UI elemanı
                                //  public RectTransform canBarFill; // Can barının doluluğunu gösteren UI elemanı

   // public Animator canAnimasyon;

    void Start()
    {
     
        currentCan = maxCan; // Karakterin başlangıç canı
        canBarSlider.maxValue = maxCan; // Slider'ın maksimum değerini ayarla
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
    public void HasarAl(int hasarMiktarı)
    {
        currentCan -= hasarMiktarı; // Hasar miktarını can değerinden çıkar

       // canAnimasyon.Play("CanAzalmaAnimasyonu");


        if (currentCan <= 0)
        {
            currentCan = 0; // Can değeri sıfıra düşerse
            // Ölme işlemleri veya diğer senaryolar burada gerçekleştirilebilir.
            // Time.timeScale = 0; // Oyunu duraklat
            // karakterin öldüğü animasyon eklenir.
            // SceneManager.LoadScene("GameOverScene");
        }

        // Can barının doluluğunu güncelle
        canBarSlider.value = currentCan;


    }
}



 