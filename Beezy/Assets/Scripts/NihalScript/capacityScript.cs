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

    public Sprite[] animImgs;
    private Image buttonImage;
    public float frameRate = 0.2f;
    private int currentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = useCapacity.GetComponent<Image>();
        player = GameObject.Find("FantasyBee");
        cpButton.onClick.AddListener(OnClick);
        InvokeRepeating("AnimateButton", frameRate, frameRate);
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

            if (distance < interactDistance)
            {
                isObjectInRange = true;

                Vector3 objPosition = magicFlower_cp.transform.position;
                Vector3 buttonPosition = Camera.main.WorldToScreenPoint(objPosition);
                buttonPosition += new Vector3(130f, 320f, 0f);

                RectTransform canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
                Vector2 viewPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, buttonPosition, null, out viewPos);
                cpButton.GetComponent<RectTransform>().anchoredPosition = viewPos;
            }
        }

        if (cpButton != null)
        {
            cpButton.gameObject.SetActive(isObjectInRange);
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
