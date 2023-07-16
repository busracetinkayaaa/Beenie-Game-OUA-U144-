using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guardScript : MonoBehaviour
{
    public GameObject player;
    public Button guardButton;
    private float interactDistance = 5f;
    public GameObject magicFlower_guard;
    public Button useGuard;
    private float minDistance = 2f;

    public Sprite[] animImgs;
    private Image buttonImage;
    public float frameRate = 0.2f;
    private int currentFrame = 0;

    public Vector2 buttonOffset = new Vector2(150f, 0f);
    private Vector3 buttonStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = useGuard.GetComponent<Image>();
        player = GameObject.Find("FantasyBee");
        guardButton.onClick.AddListener(OnClick);
        InvokeRepeating("AnimateButton", frameRate, frameRate);

        if (guardButton != null)
        {
            buttonStartPosition = guardButton.transform.position;
        }
    }

    private void AnimateButton()
    {
        if (useGuard.interactable == true)
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

        guardButton.interactable = false;
        useGuard.interactable = true;

    }

    // Update is called once per frame
    void Update()
    {
        bool isObjectInRange = false;

        if (magicFlower_guard != null)
        {
            float distance = Vector3.Distance(player.transform.position, magicFlower_guard.transform.position);

            if (distance < interactDistance && distance >= minDistance)
            {
                isObjectInRange = true;
            }
        }

        if (guardButton != null)
        {
            guardButton.gameObject.SetActive(isObjectInRange);
            if (!isObjectInRange)
            {
                guardButton.transform.position = buttonStartPosition;
            }
        }
    }

    public void SetInteractiveObject(GameObject obj)
    {
        magicFlower_guard = obj;
    }

    public void ClearInteractiveObject()
    {
        magicFlower_guard = null;
    }
}
