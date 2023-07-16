using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sahnesonudeneme : MonoBehaviour
{
    public Button deneme;
    public Animator scAnim;

    // Start is called before the first frame update
    void Start()
    {
        deneme.onClick.AddListener(dene);
    }

    public void dene()
    {
        Debug.Log("Oyun tamamlandý!");
        StartCoroutine(WaitForAnimToEnd());
    }

    IEnumerator WaitForAnimToEnd()
    {
        scAnim.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("WIN");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
