using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class enemyVol : MonoBehaviour
{
    public float lookRadius = 25f;
    Transform target;
    NavMeshAgent agent;

    private Animator animator;

    public float decreaseHealthDistance = 5f;

    private bool isAttacking = false;

    private int enemyHealth;

    public Sprite[] healthImgs;
    public Image animateHealth;

    public Button dButton;

    private bool isPlayerAttacking = false;
    private bool isPlayerInRange = false;
    public Animator playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        target = playerManager.instance.player.transform;
        playerAnimation = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.SetBool("isDead", false);
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

    void DealDamage()
    {
        // Burada hasar verme i�lemini ger�ekle�tirin, d��man�n can�n� azalt�n vb.
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

            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("isAttack", true);
                // attack the target
                // FaceTarget();
                if (distance <= decreaseHealthDistance)
                {
                    if (isPlayerAttacking && isPlayerInRange)
                    {
                        DealDamage();
                    }
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

    public void OnPlayerAttack()
    {
        isPlayerAttacking = true;
    }

    // Bu metot, oyuncunun sald�r� animasyonu tamamland���nda �a�r�l�r
    public void OnPlayerAttackComplete()
    {
        isPlayerAttacking = false;
    }

    // Bu metot, oyuncu d��mana temas etti�inde �a�r�l�r
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // Bu metot, oyuncu d��mandan ayr�ld���nda �a�r�l�r
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}