using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collide : MonoBehaviour
{

    public int maxCan =30; // Maksimum can de�eri
    public int currentCan; // Mevcut can de�eri

    public Slider canBarSlider; // Can bar� UI eleman�
                                //  public RectTransform canBarFill; // Can bar�n�n dolulu�unu g�steren UI eleman�

   // public Animator canAnimasyon;

    void Start()
    {
     
        currentCan = maxCan; // Karakterin ba�lang�� can�
        canBarSlider.maxValue = maxCan; // Slider'�n maksimum de�erini ayarla
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
    public void HasarAl(int hasarMiktar�)
    {
        currentCan -= hasarMiktar�; // Hasar miktar�n� can de�erinden ��kar

       // canAnimasyon.Play("CanAzalmaAnimasyonu");


        if (currentCan <= 0)
        {
            currentCan = 0; // Can de�eri s�f�ra d��erse
            // �lme i�lemleri veya di�er senaryolar burada ger�ekle�tirilebilir.
            // Time.timeScale = 0; // Oyunu duraklat
            // karakterin �ld��� animasyon eklenir.
            // SceneManager.LoadScene("GameOverScene");
        }

        // Can bar�n�n dolulu�unu g�ncelle
        canBarSlider.value = currentCan;


    }
}



 