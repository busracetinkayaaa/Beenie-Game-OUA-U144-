using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneChanger : MonoBehaviour
{

    public Animator sceneAnimator;

    // Update is called once per frame
    void Update()
    {

        
    }

    public void FadeToScene()
    {
        sceneAnimator.SetBool("fadeOut", true);
    }

    public void OnFadeComplete()
    {
        //SceneManager.LoadScene("");
    }
}
