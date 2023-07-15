using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOnOff : MonoBehaviour
{
    [SerializeField] public Button button;
    [SerializeField] public Sprite pressedSprite;
    [SerializeField] public Sprite defaultSprite;

    private bool isPressed = false;

    private void Start()
    {
        button.onClick.AddListener(ChangeButtonSprite);
    }

    public void ChangeButtonSprite()
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            if (isPressed)
            {
                buttonImage.sprite = defaultSprite;
            }
            else
            {
                buttonImage.sprite = pressedSprite;
            }

            isPressed = !isPressed;
        }
    }
}
