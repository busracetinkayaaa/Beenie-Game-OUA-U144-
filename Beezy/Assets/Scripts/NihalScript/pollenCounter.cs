using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pollenCounter : MonoBehaviour
{
    public Sprite[] hiveImgs;
    public Image animateHive;
    private int hiveCounter = 0;

    public Sprite[] pollImgs;
    public Image animatePoll;
    private int pollCounter = 0;

    public Button collectPol;
    public Button collectVol;
    public Button collectDes;
    public Button collectSnowy;
    public Button collectSwmp;
    public Button hiveButton;
    public Button useCapacity;


    // Start is called before the first frame update
    void Start()
    {
        // Butonlara týklama olaylarýný atayýn
        collectPol.onClick.AddListener(IncPollCounter);
        collectVol.onClick.AddListener(IncPollCounter);
        collectDes.onClick.AddListener(IncPollCounter);
        collectSnowy.onClick.AddListener(IncPollCounter);
        collectSwmp.onClick.AddListener(IncPollCounter);
        useCapacity.onClick.AddListener(IncCapacity);

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
        
    }

    private void UpdateAnimPoll()
    {
        int spriteIndex = Mathf.Clamp(pollCounter / 10, 0, pollImgs.Length - 1);
        animatePoll.sprite = pollImgs[spriteIndex];
    }
    // Update is called once per frame
    void Update()
    {

    }
}