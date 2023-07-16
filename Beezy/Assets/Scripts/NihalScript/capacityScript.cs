using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class capacityScript : MonoBehaviour
{
    public GameObject player;
    public Button cpButton;
    private float interactDistance = 8f;
    public GameObject magicFlower_cp;
    public Button useCapacity;
    private float minDistance = 3f;

    public Sprite[] animImgs;
    private Image buttonImage;
    public float frameRate = 0.2f;
    private int currentFrame = 0;

    public Vector2 buttonOffset = new Vector2(150f, 0f);
    private Vector3 buttonStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = useCapacity.GetComponent<Image>();
        player = GameObject.Find("FantasyBee");
        cpButton.onClick.AddListener(OnClick);
        InvokeRepeating("AnimateButton", frameRate, frameRate);

        if (cpButton != null)
        {
            buttonStartPosition = cpButton.transform.position;
        }
    }
    private void AnimateButton()
    {
        if (useCapacity.interactable == true)
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

        cpButton.interactable = false;
        useCapacity.interactable = true;

    }
    // Update is called once per frame
    void Update()
    {
        bool isObjectInRange = false;

        if (magicFlower_cp != null)
        {
            float distance = Vector3.Distance(player.transform.position, magicFlower_cp.transform.position);

            if (distance < interactDistance && distance >= minDistance)
            {
                isObjectInRange = true;
            }
        }

        if (cpButton != null)
        {
            cpButton.gameObject.SetActive(isObjectInRange);
            if (!isObjectInRange)
            {
                cpButton.transform.position = buttonStartPosition;
            }
        }
    }
    public void SetInteractiveObject(GameObject obj)
    {
        magicFlower_cp = obj;
    }

    public void ClearInteractiveObject()
    {
        magicFlower_cp = null;
    }
}
