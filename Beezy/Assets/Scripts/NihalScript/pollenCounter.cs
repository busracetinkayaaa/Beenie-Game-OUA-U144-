using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pollenCounter : MonoBehaviour
{
    public GameObject player;
    public GameObject hive;
    private float interactDistance = 10f;

    public Sprite[] hiveImgs;
    public Image animateHive;
    public Button hiveButton;
    private int hiveCounter = 0;

    public Sprite[] pollImgs;
    public Image animatePoll;
    private int pollCounter = 0;

    public Button collectPol;
    public Button collectVol;
    public Button collectDes;
    public Button collectSnowy;
    public Button collectSwmp;

    public Button useCapacity;
    private RectTransform canvasRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FantasyBee");

        animateHive.sprite = hiveImgs[0];
        animatePoll.sprite = pollImgs[0];

        canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        hiveButton.gameObject.SetActive(false);

        collectPol.onClick.AddListener(IncPollCounter);
        collectVol.onClick.AddListener(IncPollCounter);
        collectDes.onClick.AddListener(IncPollCounter);
        collectSnowy.onClick.AddListener(IncPollCounter);
        collectSwmp.onClick.AddListener(IncPollCounter);
        useCapacity.onClick.AddListener(IncCapacity);

        hiveButton.onClick.AddListener(PollToHive);

    }

    public void IncPollCounter()
    {
        if (useCapacity.interactable)
        {
            if (pollCounter < 150)
            {
                pollCounter += 10;
                if (pollCounter >= 150)
                {
                    collectPol.interactable = false;
                    collectVol.interactable = false;
                    collectDes.interactable = false;
                    collectSnowy.interactable = false;
                    collectSwmp.interactable = false;
                    useCapacity.interactable = false;
                }
            }
        }
        else
        {
            if (pollCounter < 100)
            {
                pollCounter += 10;
                if (pollCounter >= 100)
                {
                    collectPol.interactable = false;
                    collectVol.interactable = false;
                    collectDes.interactable = false;
                    collectSnowy.interactable = false;
                    collectSwmp.interactable = false;
                }
            }
        }
        UpdateAnimPoll();
    }

    public void IncCapacity()
    {
        if (useCapacity.interactable)
        {
            if (pollCounter < 150)
            {
                pollCounter += 10;
                if (pollCounter >= 150)
                {
                    collectPol.interactable = false;
                    collectVol.interactable = false;
                    collectDes.interactable = false;
                    collectSnowy.interactable = false;
                    collectSwmp.interactable = false;
                    useCapacity.interactable = false;
                }
            }
            UpdateAnimPoll();
        }
    }

    private void UpdateAnimHive()
    {
        int spriteIndex = Mathf.Clamp(hiveCounter / 25, 0, hiveImgs.Length - 1);
        animateHive.sprite = hiveImgs[spriteIndex];

        if (spriteIndex == hiveImgs.Length - 1 || pollCounter == 0)
        {
            hiveButton.interactable = false;
        }
        if ( hiveCounter!=0 && hiveCounter % 25 == 0 && spriteIndex < hiveImgs.Length - 1)
        {
            animateHive.sprite = hiveImgs[spriteIndex + 1];
        }
    }
    private void UpdateAnimPoll()
    {
        int spriteIndex = Mathf.Clamp(pollCounter / 10, 0, pollImgs.Length - 1);
        animatePoll.sprite = pollImgs[spriteIndex];
    }

    public void PollToHive()
    {
        hiveCounter = pollCounter;
        UpdateAnimHive();
        if (pollCounter == 100 || pollCounter == 150)
        {
            hiveButton.gameObject.SetActive(true);
            hiveButton.interactable = true;
        }
        pollCounter = 0;
        animatePoll.sprite = pollImgs[0];
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, hive.transform.position);

        if (distance <= interactDistance)
        {
            Vector3 buttonScreenPosition = Camera.main.WorldToScreenPoint(hive.transform.position);
            buttonScreenPosition += new Vector3(-150f, 0f, 0f);

            Vector2 viewPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, buttonScreenPosition, null, out viewPos);
            hiveButton.GetComponent<RectTransform>().anchoredPosition = viewPos;
            hiveButton.gameObject.SetActive(true);
        }
        else
        {
            hiveButton.gameObject.SetActive(false);
        }
    }

}