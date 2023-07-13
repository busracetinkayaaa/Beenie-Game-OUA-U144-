using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class enemies : MonoBehaviour
{
    private int health = 100;
    private int currentHealth; 
    public Sprite[] healthImgs;
    public Image animateHealth;

    public float lookRadius = 25f;
    Transform target;
    public NavMeshAgent agentVol;
    public NavMeshAgent agentDes;
    public NavMeshAgent agentSnow;
    public NavMeshAgent agentSwamp;

    public Button colGuard; // des
    public Button colSpeed; // snow
    public Button colDamage; // vol
    public Button colCapacity; // swmp

    private Animator animator;

    public float decreaseHealthDistance = 5f;

    private bool isAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        target = playerManager.instance.player.transform;

        agentVol = GetComponent<NavMeshAgent>();
        agentDes = GetComponent<NavMeshAgent>();
        agentSnow = GetComponent<NavMeshAgent>();
        agentSwamp = GetComponent<NavMeshAgent>();

        colGuard.interactable = false;
        colSpeed.interactable = false;
        colDamage.interactable = false;
        colCapacity.interactable = false;

        animator = GetComponent<Animator>();

    }

    private void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    // Update is called once per frame
    void Update()
    {
        float distanceVol = Vector3.Distance(agentVol.transform.position, target.position);
        float distanceDes = Vector3.Distance(agentDes.transform.position, target.position);
        float distanceSnow = Vector3.Distance(agentSnow.transform.position, target.position);
        float distanceSwamp = Vector3.Distance(agentSwamp.transform.position, target.position);

        // Agent 1 
        if (distanceVol > lookRadius)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }
        else if (distanceVol <= lookRadius)
        {
            agentVol.SetDestination(target.position);

            if (distanceVol <= agentVol.stoppingDistance)
            {
                animator.SetBool("isAttack", true);
         
                if (distanceVol <= decreaseHealthDistance)
                {
                    int currentHealthVol = (int)healthBar.instance.GetHealth();
                    int updatedHealthVol = currentHealthVol - 20;
                    healthBar.instance.SetHealth(updatedHealthVol);
                }
            }
            else
            {
                animator.SetBool("isAttack", false);
                animator.SetBool("isWalk", true);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }

        // Agent 2
        if (distanceDes > lookRadius)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }
        else if (distanceDes <= lookRadius)
        {
            agentDes.SetDestination(target.position);

            if (distanceDes <= agentDes.stoppingDistance)
            {
                animator.SetBool("isAttack", true);
               
                if (distanceDes <= decreaseHealthDistance)
                {
                    int currentHealthDes = (int)healthBar.instance.GetHealth();
                    int updatedHealthDes = currentHealthDes - 20;
                    healthBar.instance.SetHealth(updatedHealthDes);
                }
            }
            else
            {
                animator.SetBool("isAttack", false);
                animator.SetBool("isWalk", true);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }

        // Agent 3 
        if (distanceSnow > lookRadius)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }
        else if (distanceSnow <= lookRadius)
        {
            agentSnow.SetDestination(target.position);

            if (distanceSnow <= agentSnow.stoppingDistance)
            {
                animator.SetBool("isAttack", true);
                
                if (distanceSnow <= decreaseHealthDistance)
                {
                    int currentHealthSnow = (int)healthBar.instance.GetHealth();
                    int updatedHealthSnow = currentHealthSnow - 20;
                    healthBar.instance.SetHealth(updatedHealthSnow);
                }
            }
            else
            {
                animator.SetBool("isAttack", false);
                animator.SetBool("isWalk", true);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }

        // Agent 4 
        if (distanceSwamp > lookRadius)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }
        else if (distanceSwamp <= lookRadius)
        {
            agentSwamp.SetDestination(target.position);

            if (distanceSwamp <= agentSwamp.stoppingDistance)
            {
                animator.SetBool("isAttack", true);
                
                if (distanceSwamp <= decreaseHealthDistance)
                {
                    int currentHealthSwamp = (int)healthBar.instance.GetHealth();
                    int updatedHealthSwamp = currentHealthSwamp - 20;
                    healthBar.instance.SetHealth(updatedHealthSwamp);
                }
            }
            else
            {
                animator.SetBool("isAttack", false);
                animator.SetBool("isWalk", true);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }

    }
}
