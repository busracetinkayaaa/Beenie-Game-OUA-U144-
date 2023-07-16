using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageScript : MonoBehaviour
{
    public GameObject player;
    public Button damageButton;
    private float interactDistance = 15f;
    public GameObject magicFlower_damage;
    public Button useDamage;
    private float minDistance = 9f;

    public Sprite[] animImgs;
    private Image buttonImage;
    public float frameRate = 0.2f;
    private int currentFrame = 0;

    public Vector2 buttonOffset = new Vector2(150f, 0f);
    private Vector3 buttonStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = useDamage.GetComponent<Image>();
        player = GameObject.Find("FantasyBee");
        damageButton.onClick.AddListener(OnClick);
        InvokeRepeating("AnimateButton", frameRate, frameRate);

        if (damageButton != null)
        {
            buttonStartPosition = damageButton.transform.position;
        }
    }

    private void AnimateButton()
    {
        if (useDamage.interactable == true)
        {
            currentFrame++;
            if (currentFrame >= animImgs.Length)
            {
                currentFrame = 0;
            }

            buttonImage.sprite = animImgs[currentFrame];

        }
    }
    private void OnClick()
    {

        damageButton.interactable = false;
        useDamage.interactable = true;

    }

    // Update is called once per frame
    void Update()
    {
        bool isObjectInRange = false;

        if (magicFlower_damage != null)
        {
            float distance = Vector3.Distance(player.transform.position, magicFlower_damage.transform.position);

            if (distance < interactDistance && distance >= minDistance)
            {
                isObjectInRange = true;
            }
        }

        if (damageButton != null)
        {
            damageButton.gameObject.SetActive(isObjectInRange);
            if (!isObjectInRange)
            {
                damageButton.transform.position = buttonStartPosition;
            }
        }
    }

    public void SetInteractiveObject(GameObject obj)
    {
        magicFlower_damage = obj;
    }

    public void ClearInteractiveObject()
    {
        magicFlower_damage = null;
    }
}
