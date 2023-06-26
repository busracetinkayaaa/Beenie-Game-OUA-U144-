using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float can;
    public bool dead = false;
    

    public void HasarAl(float hasarMiktarı)
    {
        if (can - hasarMiktarı >= 0)
        {
            can -= hasarMiktarı;
            // Can barının doluluğunu güncelle
            //canBarSlider.value = can;
        }
        else
        {
            can = 0;
        }
        isDead();
    }

    void isDead()
    {
        if (can == 0)
            dead = true;
    }
}
