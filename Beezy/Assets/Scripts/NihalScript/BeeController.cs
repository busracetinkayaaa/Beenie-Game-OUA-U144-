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

    public float forwardSpeed = 15f, strafeSpeed = 7f, hoverSpeed = 5f;
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
    public bool isShieldActive = false;

    public GameObject dmg;
    private GameObject dmgE;
    public bool isDmgActive = false;

    public bool isAttackingPlayer = false;

    void Start()
    {
        player = GameObject.Find("FantasyBee");
        //target = playerManager.instance.player.transform;

        animator = GetComponent<Animator>();
        animator.SetFloat("isdead", 0.5f);
        targetRotation = transform.rotation;
        smoothRotation = transform.rotation;

        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        gButton.onClick.AddListener(gOnClick);
        sButton.onClick.AddListener(sOnClick);
        dButton.onClick.AddListener(dOnClick);
        cButton.onClick.AddListener(cOnClick);

        useGuard.onClick.AddListener(guardOnClick);
        useSpeed.onClick.AddListener(speedOnClick);
        useDamage.onClick.AddListener(damageOnClick);

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

    private void damageOnClick()
    {
        StartCoroutine(dmgEff());
        useDamage.interactable = false;
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


        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("IsAttack", true);
            isAttackingPlayer = true;

}

        if (!Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("IsAttack", false);
            isAttackingPlayer = false;
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

        if (Input.GetMouseButton(1))
        {
            mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
            transform.Rotate(0f, mouseDistance.x * lookRateSpeed * Time.deltaTime, 0f, Space.Self);
        }
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);  
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strateAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

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

    private IEnumerator dmgEff()
    {
        Vector3 dmgPosition = player.transform.position;
        float dmgHeight = shield.GetComponent<Renderer>().bounds.size.y;
        float yOffset = (player.transform.lossyScale.y + dmgHeight) / 2f;
        dmgPosition.y += yOffset / 2;

        dmgE = Instantiate(dmg, dmgPosition, player.transform.rotation);
        dmgE.transform.parent = player.transform;

        isDmgActive = true;

        yield return new WaitForSeconds(15f);

        Destroy(dmgE);
        isDmgActive = false;

        yield return null;
    }

}
