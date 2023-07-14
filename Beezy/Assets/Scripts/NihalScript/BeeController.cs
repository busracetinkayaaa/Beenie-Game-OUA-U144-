using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BeeController : MonoBehaviour
{
    // vertical = w-s (forward)
    // horizontal = a-d (strafe)
    // hover = space-altctrl (hover)

    public float forwardSpeed = 7f, strafeSpeed = 7f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 0.3f, strateAcceleration = 0.5f, hoverAcceleration = 0.5f;

    private Animator animator;

    public float lookRateSpeed = 40f;
    private Vector2 lookInput, screenCenter, mouseDistance;


    // tilt
    public float tiltAngle = 20f;
    public float turnSpeed = 3f;
    public float turnSmoothness = 5f;
    private Quaternion targetRotation;
    private Quaternion smoothRotation;

    // buttons
    public Button gButton; // guard
    public Button cButton; // capacity 
    public Button sButton; // speed
    public Button dButton; // damage

    public Button useGuard;
    public Button useSpeed;
    public Button useDamage; 

    public GameObject player;
    public GameObject shield;
    private GameObject shieldE;
    private bool isShieldActive = false;

    /*
    // enemy 
    public NavMeshAgent agentVol, agentDes, agentSnw, agentSwmp;
    private int volEnemyHealth, desEnemyHealth, snwEnemyHealth, swmpEnemyHealth = 80;
    float distanceVol, distanceDes, distanceSnw, distanceSwmp;
    Transform target;

    public float attackRange = 5f;
    public float lookRadius = 25f;

    public Sprite[] healthImgs;

    public Image animateHealthVol;
    public Image animateHealthDes;
    public Image animateHealthSnw;
    public Image animateHealthSwmp;

    public Animator volAnim;
    public Animator desAnim;
    public Animator snwAnim;
    public Animator swmpAnim;

    private bool isAttackingPlayer = false;
    private bool isAttackingEnemy = false;
    private bool isPlayerInRange = false;

    int healthVol;
    int healthDes;
    int healthSnw;
    int healthSwmp; */

    void Start()
    {
        player = GameObject.Find("FantasyBee");
        //target = playerManager.instance.player.transform;

        animator = GetComponent<Animator>();
        targetRotation = transform.rotation;
        smoothRotation = transform.rotation;

        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        gButton.interactable = false;
        sButton.interactable = false;
        dButton.interactable = false;
        cButton.interactable = false;

        gButton.onClick.AddListener(gOnClick);
        sButton.onClick.AddListener(sOnClick);
        dButton.onClick.AddListener(dOnClick);
        cButton.onClick.AddListener(cOnClick);

        useGuard.onClick.AddListener(guardOnClick);
        useSpeed.onClick.AddListener(speedOnClick);

        //agentVol = GetComponent<NavMeshAgent>();
        //agentDes = GetComponent<NavMeshAgent>();
        //agentSnw = GetComponent<NavMeshAgent>();
        //agentSwmp = GetComponent<NavMeshAgent>();


    }
    private void guardOnClick()
    {
        StartCoroutine(shieldEff());
        useGuard.interactable = false;
    }

    private void speedOnClick()
    {
        StartCoroutine(IncreaseSpeed());
        useSpeed.interactable = false;
    }

    void Update()
    {   
        /////////////////////////////////////
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.LeftControl) )
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }


        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("IsAttack", true);

        }

        if (!Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("IsAttack", false);
        }

        /*if (Input.GetKey(KeyCode.A))
        {
            // Sola dönme
            targetRotation = Quaternion.Euler(0f, 0f, tiltAngle/3);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Saða dönme
            targetRotation = Quaternion.Euler(0f, 0f, -tiltAngle/3);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            // Dönme yoksa
            targetRotation = Quaternion.identity;
        }
        smoothRotation = Quaternion.Lerp(smoothRotation, targetRotation, turnSmoothness * Time.deltaTime);
        transform.rotation = smoothRotation;
        */
        //
        /*lookInput.x = Input.mousePosition.x;
        //lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        //mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        transform.Rotate(0f, mouseDistance.x * lookRateSpeed * Time.deltaTime, 0f, Space.Self);
        */
        Vector2 lookInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouseDistance = (lookInput - screenCenter) / screenCenter;

        if (Input.GetMouseButton(0))
        {
            mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
            transform.Rotate(0f, mouseDistance.x * lookRateSpeed * Time.deltaTime, 0f, Space.Self);
        }
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);  
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strateAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

        /////////////////////////////////
        /*
        float distanceVol = Vector3.Distance(target.position, agentVol.transform.position);

        if (distanceVol > lookRadius)
        {
            volAnim.SetBool("isWalk", false);
            volAnim.SetBool("isAttack", false);
        }
        else if (distanceVol <= lookRadius)
        {
            agentVol.SetDestination(target.position);

            if (distanceVol <= agentVol.stoppingDistance)
            {
                animator.SetBool("isAttack", true);
                // attack the target
                // FaceTarget();
                if (distanceVol <= attackRange)
                {
                    if (isAttackingPlayer && isPlayerInRange)
                    {
                        //
                    }
                }
            }
            else
            {
                volAnim.SetBool("isAttack", false);
                volAnim.SetBool("isWalk", true);
            }
        }
        else
        {
            volAnim.SetBool("isWalk", false);
            volAnim.SetBool("isAttack", false);
        }

        float distanceDes = Vector3.Distance(target.position, agentDes.transform.position);

        if (distanceDes > lookRadius)
        {
            desAnim.SetBool("isWalk", false);
            desAnim.SetBool("isAttack", false);
        }
        else if (distanceDes <= lookRadius)
        {
            agentDes.SetDestination(target.position);

            if (distanceDes <= agentDes.stoppingDistance)
            {
                animator.SetBool("isAttack", true);
                // attack the target
                // FaceTarget();
                if (distanceDes <= attackRange)
                {
                    if (isAttackingPlayer && isPlayerInRange)
                    {
                        //
                    }
                }
            }
            else
            {
                desAnim.SetBool("isAttack", false);
                desAnim.SetBool("isWalk", true);
            }
        }
        else
        {
            desAnim.SetBool("isWalk", false);
            desAnim.SetBool("isAttack", false);
        }


        float distanceSnw = Vector3.Distance(target.position, agentSnw.transform.position);

        if (distanceSnw > lookRadius)
        {
            snwAnim.SetBool("isWalk", false);
            snwAnim.SetBool("isAttack", false);
        }
        else if (distanceSnw <= lookRadius)
        {
            agentSnw.SetDestination(target.position);

            if (distanceSnw <= agentSnw.stoppingDistance)
            {
                snwAnim.SetBool("isAttack", true);
                // attack the target
                // FaceTarget();
                if (distanceSnw <= attackRange)
                {
                    if (isAttackingPlayer && isPlayerInRange)
                    {
                        //
                    }
                }
            }
            else
            {
                snwAnim.SetBool("isAttack", false);
                snwAnim.SetBool("isWalk", true);
            }
        }
        else
        {
            snwAnim.SetBool("isWalk", false);
            snwAnim.SetBool("isAttack", false);
        }


        float distanceSwmp = Vector3.Distance(target.position, agentSwmp.transform.position);

        Debug.Log("distance Swamp" + distanceSwmp);
        if (distanceSwmp > lookRadius)
        {
            swmpAnim.SetBool("isWalk", false);
            swmpAnim.SetBool("isAttack", false);
        }
        else if (distanceSwmp <= lookRadius)
        {
            agentSwmp.SetDestination(target.position);

            if (distanceSwmp <= agentSwmp.stoppingDistance)
            {
                swmpAnim .SetBool("isAttack", true);
                // attack the target
                // FaceTarget();
                if (distanceSwmp <= attackRange)
                {
                    if (isAttackingPlayer && isPlayerInRange)
                    {
                        //
                    }
                }
            }
            else
            {
                swmpAnim.SetBool("isAttack", false);
                swmpAnim.SetBool("isWalk", true);
            }
        }
        else
        {
            swmpAnim.SetBool("isWalk", false);
            swmpAnim.SetBool("isAttack", false);
        } */
    }

    private void gOnClick()
    {
        gButton.interactable = false;
    }

    private void sOnClick()
    {
        sButton.interactable = false;

    }

    private void dOnClick()
    {
        dButton.interactable = false;
    }

    private void cOnClick()
    {
        cButton.interactable = false;
    }

    private IEnumerator IncreaseSpeed()
    {
        forwardSpeed *= 2f;
        strafeSpeed *= 2f;
        hoverSpeed *= 2f;

        yield return new WaitForSeconds(15f);
        forwardSpeed /= 2f;
        strafeSpeed /= 2f;
        hoverSpeed /= 2f;

        yield return null;
    }

    private IEnumerator shieldEff()
    {
        Vector3 shieldPosition = player.transform.position;
        float shieldHeight = shield.GetComponent<Renderer>().bounds.size.y;
        float yOffset = (player.transform.lossyScale.y + shieldHeight) / 2f;
        shieldPosition.y += yOffset / 2;

        shieldE = Instantiate(shield, shieldPosition, player.transform.rotation);
        shieldE.transform.parent = player.transform;

        isShieldActive = true;

        yield return new WaitForSeconds(15f);

        Destroy(shieldE);
        isShieldActive = false;

        yield return null;
    }

}
