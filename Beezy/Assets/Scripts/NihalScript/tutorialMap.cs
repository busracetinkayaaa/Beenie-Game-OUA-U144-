using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class tutorialMap : MonoBehaviour
{
    public Sprite[] openImgs;
    public Sprite[] closeImgs;
    public Image tutorialImg;

    public Image[] listedImgs;

    public Button openButt;
    public Button closeButt;
    public Button leftButt;
    public Button rightButt;

    private int currentIndex = -1;
    private bool isAnimating = false;
    // Start is called before the first frame update
    private void Start()
    {
        openButt.onClick.AddListener(PlayOpenAnimation);
        closeButt.onClick.AddListener(PlayCloseAnimation);
        leftButt.onClick.AddListener(ShowPreviousImage);
        rightButt.onClick.AddListener(ShowNextImage);

        tutorialImg.gameObject.SetActive(false);
        DisableButton(leftButt);
        DisableButton(rightButt);
        DisableButton(closeButt);
        leftButt.gameObject.SetActive(false);
        rightButt.gameObject.SetActive(false);
        closeButt.gameObject.SetActive(false);

        // Baþlangýçta listedeki tüm görüntüleri kapalý hale getir
        foreach (Image img in listedImgs)
        {
            img.gameObject.SetActive(false);
        }
    }

    private void PlayOpenAnimation()
    {
        if (isAnimating) return;

        StartCoroutine(AnimateImages(openImgs, () =>
        {
            tutorialImg.gameObject.SetActive(false);
            currentIndex = 0;
            ShowImage(currentIndex); // Ýlk görüntüyü göster
            EnableButton(leftButt);
            EnableButton(rightButt);
            EnableButton(closeButt);
            leftButt.gameObject.SetActive(true);
            rightButt.gameObject.SetActive(true);
            closeButt.gameObject.SetActive(true);
        }));
    }

    private void PlayCloseAnimation()
    {
        if (isAnimating) return;

        if (currentIndex >= 0 && currentIndex < listedImgs.Length)
        {
            listedImgs[currentIndex].gameObject.SetActive(false);
        }

        StartCoroutine(AnimateImages(closeImgs, () =>
        {
            tutorialImg.gameObject.SetActive(false);
            currentIndex = -1;
            ShowImage(currentIndex);
            leftButt.gameObject.SetActive(false);
            rightButt.gameObject.SetActive(false);
            closeButt.gameObject.SetActive(false);
            DisableButton(leftButt);
            DisableButton(rightButt);
            DisableButton(closeButt);
            foreach (Image img in listedImgs)
            {
                img.gameObject.SetActive(false);
            }
        }));
    }

    private void ShowNextImage()
    {
        if (isAnimating) return;

        if (currentIndex + 1 < listedImgs.Length)
        {
            currentIndex++;
            ShowImage(currentIndex);
            if (currentIndex == listedImgs.Length - 1)
            {
                DisableButton(rightButt);
            }
            EnableButton(leftButt);
        }
        else
        {
            DisableButton(rightButt);
        }
    }

    private void ShowPreviousImage()
    {
        if (isAnimating) return;

        if (currentIndex > 0)
        {
            currentIndex--;
            ShowImage(currentIndex);
            if (currentIndex == 0)
            {
                DisableButton(leftButt);
            }
            if (currentIndex < listedImgs.Length - 1)
            {
                EnableButton(rightButt);
            }
        }
        else
        {
            DisableButton(leftButt);
        }
    }

    private void ShowImage(int index)
    {
        // Tüm görüntüleri kapat
        foreach (Image img in listedImgs)
        {
            img.gameObject.SetActive(false);
        }

        // Belirli görüntüyü göster
        if (index >= 0 && index < listedImgs.Length)
        {
            listedImgs[index].gameObject.SetActive(true);
        }
    }

    private IEnumerator AnimateImages(Sprite[] images, System.Action onComplete)
    {
        isAnimating = true;
        tutorialImg.gameObject.SetActive(true);

        foreach (Sprite img in images)
        {
            tutorialImg.sprite = img;
            yield return new WaitForSeconds(0.1f);
        }

        onComplete?.Invoke();
        isAnimating = false;
    }

    private void EnableButton(Button button)
    {
        button.interactable = true;
    }

    private void DisableButton(Button button)
    {
        button.interactable = false;
    }
}



