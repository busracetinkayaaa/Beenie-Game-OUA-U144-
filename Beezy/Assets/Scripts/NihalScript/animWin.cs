using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class animWin : MonoBehaviour
{
    public Sprite[] animImgs;
    public Image animateImgObj;

    public Animator scAnim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransitionToMainMenu());
    }
    private System.Collections.IEnumerator TransitionToMainMenu()
    {
        yield return new WaitForSeconds(4f);
        scAnim.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenuScene");
    }
    // Update is called once per frame
    void Update()
    {
        animateImgObj.sprite = animImgs[(int)(Time.time * 7) % animImgs.Length];
    }
}
