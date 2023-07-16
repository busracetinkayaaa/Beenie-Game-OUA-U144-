using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedScript : MonoBehaviour
{
    public GameObject player;
    public Button speedButton;
    private float interactDistance = 15f;
    public GameObject magicFlower_speed;
    public Button useSpeed;
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
        buttonImage = useSpeed.GetComponent<Image>();
        player = GameObject.Find("FantasyBee");
        speedButton.onClick.AddListener(OnClick);
        InvokeRepeating("AnimateButton", frameRate, frameRate);

        if (speedButton != null)
        {
            buttonStartPosition = speedButton.transform.position;
        }

    }
    private void AnimateButton()
    {
        if (useSpeed.interactable == true)
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

        speedButton.interactable = false;
        useSpeed.interactable = true;

    }

    // Update is called once per frame
    void Update()
    {
        bool isObjectInRange = false;

        if (magicFlower_speed != null)
        {
            float distance = Vector3.Distance(player.transform.position, magicFlower_speed.transform.position);

            if (distance < interactDistance && distance >= minDistance)
            {
                isObjectInRange = true;
            }
        }

        if (speedButton != null)
        {
            speedButton.gameObject.SetActive(isObjectInRange);
            if (!isObjectInRange)
            {
                speedButton.transform.position = buttonStartPosition;
            }
        }
    }

    public void SetInteractiveObject(GameObject obj)
    {
        magicFlower_speed = obj;
    }

    public void ClearInteractiveObject()
    {
        magicFlower_speed = null;
    }
}
