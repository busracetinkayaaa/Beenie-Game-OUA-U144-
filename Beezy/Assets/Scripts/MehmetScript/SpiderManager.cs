using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderManager : MonoBehaviour
{
    public float can;

    public float hasarMiktarı;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().HasarAl(hasarMiktarı);                
        }
    }

}


