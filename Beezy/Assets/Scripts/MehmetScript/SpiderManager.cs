using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpiderManager : MonoBehaviour
{
    public float can;

    public float hasarMiktarı;
    public float yakalamaMesafe;
    public float saldırmaMesafe;
    
    NavMeshAgent spiderNavMesh;
    Animator spiderAnim;
    private Vector3 hedefPosition;

    private GameObject hedefOyuncu;
    

    void Start()
    {
        spiderAnim = this.GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("Player");
        spiderNavMesh = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        hedefPosition = hedefOyuncu.transform.position;
        hedefPosition.y = this.transform.position.y; // Yüksekliği aynı seviyeye getir
        
        
        float mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);
        
        if (mesafe < yakalamaMesafe)
        {
            this.transform.LookAt(hedefPosition);
            spiderNavMesh.isStopped = false;
            spiderNavMesh.SetDestination(hedefOyuncu.transform.position);
            //AnimYürüme;
            spiderAnim.SetBool("walking", true);
            spiderAnim.SetBool("attack", false);
            
        }
        else
        {
            spiderNavMesh.isStopped = true;
            //AnimDurma;
            spiderAnim.SetBool("walking", false);
            spiderAnim.SetBool("attack", false);
        }

        if (mesafe < saldırmaMesafe)
        {
            this.transform.LookAt(hedefPosition);
            spiderNavMesh.isStopped = true;
            //AnimSaldırma;
            spiderAnim.SetBool("attack", true);
            spiderAnim.SetBool("walking", false);
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().HasarAl(hasarMiktarı);                
        }
    }

}


