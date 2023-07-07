using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guardScript : MonoBehaviour
{
    public GameObject player;
    public Button guardButton;
    private float interactDistance = 2f;
    public GameObject magicFlower_guard;
    public Button useGuard;

    public Sprite[] animImgs;
    private Image buttonImage;
    public float frameRate = 0.2f;
    private int currentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = useGuard.GetComponent<Image>();
        player = GameObject.Find("FantasyBee");
        guardButton.onClick.AddListener(OnClick);
        InvokeRepeating("AnimateButton", frameRate, frameRate);
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

            if (distance < interactDistance)
            {
                isObjectInRange = true;

                Vector3 objPosition = magicFlower_guard.transform.position;
                Vector3 buttonPosition = Camera.main.WorldToScreenPoint(objPosition);
                buttonPosition += new Vector3(100f, 130f, 0f);

                RectTransform canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
                Vector2 viewPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, buttonPosition, null, out viewPos);
                guardButton.GetComponent<RectTransform>().anchoredPosition = viewPos;
            }
        }

        if (guardButton != null)
        {
            guardButton.gameObject.SetActive(isObjectInRange);
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
