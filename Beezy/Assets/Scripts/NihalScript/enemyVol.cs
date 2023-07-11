using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyVol : MonoBehaviour
{
    public float lookRadius = 15f;
    Transform target;
    NavMeshAgent agent;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = playerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }



    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance > lookRadius)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }
        else if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("isWalk", true);

            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("isAttack", true);
                // attack the target
                // FaceTarget();
            }
        }
        else
        {
            animator.SetBool("isWalk", false);

        }
    }
}
