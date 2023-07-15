using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static BeeController;

public class enemyVol : MonoBehaviour
{
    public float lookRadius = 30f;
    Transform target;
    NavMeshAgent agent;

    public Animator animator;
    float dieAnimationLength;

    public float decreaseHealthDistance = 5f;


    public static enemyVol instanceVol;
    public static int volEnemyHealth = 80;

    public Sprite[] healthImgs;
    public Image animateHealth;

    public Button dButton;

    public Animator beeAnim;
    public GameObject player;
    public GameObject vfxPrefab;
    float offsetZ = 2f;
    GameObject vfx = null;

    public BeeController beeController;
 
        // Start is called before the first frame update
    void Start()
    {
        beeController = GameObject.Find("FantasyBee").GetComponent<BeeController>();
        vfxPrefab.SetActive(false);
        //Debug.Log("isAttackingPlayer deðeri: " + beeController.isAttackingPlayer);
        animateHealth.gameObject.SetActive(false);

        target = playerManager.instance.player.transform;
        beeAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        dButton.interactable = false;
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
    public void BeeAttack(float distance, float decreaseHealthDistance)
    {
        if (beeController.isAttackingPlayer == true)
        {
            if (distance <= decreaseHealthDistance)
            {
                //Debug.Log("BeeAttack");
                if (vfx == null)
                {
                    vfx = Instantiate(vfxPrefab, target.position, Quaternion.identity);
                }
                vfx.SetActive(true);
                Vector3 direction = agent.transform.position - target.position;
                Vector3 initialPosition = new Vector3(target.position.x, target.position.y, target.position.z);

                vfx.transform.position = initialPosition;
                vfx.transform.LookAt(agent.transform.position + direction);

                Invoke("DeactivateVFX", 5f);

                DealDamage();
            }
            else 
            {
                //Debug.Log("BeeAttack");
                if (vfx == null)
                {
                    vfx = Instantiate(vfxPrefab, target.position, Quaternion.identity);
                }
                vfx.SetActive(true);
                Vector3 direction = agent.transform.position - target.position;
                Vector3 initialPosition = new Vector3(target.position.x, target.position.y, target.position.z);

                vfx.transform.position = initialPosition;
                vfx.transform.LookAt(agent.transform.position + direction);

                Invoke("DeactivateVFX", 5f);

            }
        }
       
    }

    void DeactivateVFX()
    {
        if (vfx != null)
        {
            vfx.SetActive(false);
        }
    }

    void DealDamage()
    {
        if (beeController.isDmgActive)
        {
            volEnemyHealth = 0;
            StartCoroutine(PlayDieAnimationAndDestroy());
        }
        else
        {
            volEnemyHealth -= 20;

            if (volEnemyHealth <= 0) 
            {
                volEnemyHealth = 0;
                StartCoroutine(PlayDieAnimationAndDestroy());

            }
          
            UpdateHealth(volEnemyHealth);

        }
    }
    void UpdateHealth(int health)
    {
        int index = 0;

        // Saðlýk deðerine göre index deðerini belirle
        if (health >= 80)
        {
            index = 0;
        }
        else if (health >= 60)
        {
            index = 1;
        }
        else if (health >= 40)
        {
            index = 2;
        }
        else if (health >= 20)
        {
            index = 3;
        }
        else if (health == 0)
        {
            index = 4;
        }

        animateHealth.sprite = healthImgs[index];

        Debug.Log("Health: " + health);

    }

    IEnumerator PlayDieAnimationAndDestroy()
    {
        animator.SetBool("isDead", true);

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        float dieAnimationLength = 0f;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "dieAnimation")
            {
                dieAnimationLength = clip.length;
                break;
            }
        }

        yield return new WaitForSeconds(3f);

        Destroy(agent.gameObject);
        animateHealth.gameObject.SetActive(false);
        dButton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > lookRadius)
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }
        else if (distance <= lookRadius)
        {
            animateHealth.gameObject.SetActive(true);
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("isAttack", true);        
            }
            else
            {
                animator.SetBool("isAttack", false);
                animator.SetBool("isWalk", true);
            }
            if (distance <= decreaseHealthDistance)
            {
                //Debug.Log("isAttackingPlayer deðeri: " + beeController.isAttackingPlayer);
                BeeAttack(distance, decreaseHealthDistance);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }
    }

}