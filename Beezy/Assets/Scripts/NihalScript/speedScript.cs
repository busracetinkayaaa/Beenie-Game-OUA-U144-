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

    public Sprite[] animImgs;
    private Image buttonImage;
    public float frameRate = 0.2f;
    private int currentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = useSpeed.GetComponent<Image>();
        player = GameObject.Find("FantasyBee");
        speedButton.onClick.AddListener(OnClick);
        InvokeRepeating("AnimateButton", frameRate, frameRate);

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

            if (distance < interactDistance)
            {
                isObjectInRange = true;

                Vector3 objPosition = magicFlower_speed.transform.position;
                Vector3 buttonPosition = Camera.main.WorldToScreenPoint(objPosition);
                buttonPosition += new Vector3(150f, 500f, 0f);

                RectTransform canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
                Vector2 viewPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, buttonPosition, null, out viewPos);
                speedButton.GetComponent<RectTransform>().anchoredPosition = viewPos;
            }
        }
        if (speedButton != null)
        {
            speedButton.gameObject.SetActive(isObjectInRange);
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
