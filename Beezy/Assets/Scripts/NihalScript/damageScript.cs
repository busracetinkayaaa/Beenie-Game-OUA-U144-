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

    public Sprite[] animImgs;
    private Image buttonImage;
    public float frameRate = 0.2f;
    private int currentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {

        buttonImage = useDamage.GetComponent<Image>();
        player = GameObject.Find("FantasyBee");
        damageButton.onClick.AddListener(OnClick);
        InvokeRepeating("AnimateButton", frameRate, frameRate);
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

            if (distance < interactDistance)
            {
                isObjectInRange = true;

                Vector3 objPosition = magicFlower_damage.transform.position;
                Vector3 buttonPosition = Camera.main.WorldToScreenPoint(objPosition);
                buttonPosition += new Vector3(230f, 320f, 0f);

                RectTransform canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
                Vector2 viewPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, buttonPosition, null, out viewPos);
                damageButton.GetComponent<RectTransform>().anchoredPosition = viewPos;
            }
        }

        if (damageButton != null)
        {
            damageButton.gameObject.SetActive(isObjectInRange);
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
