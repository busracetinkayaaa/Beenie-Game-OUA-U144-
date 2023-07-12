using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;

    public void LoadLevel(string Busra )
    {
        SceneManager.LoadScene("Busra");
    }

    public void LoadLevelSettings(string Settings)
    {
        SceneManager.LoadScene("Settings");
    }
    public void LoadLevelEnd(string GameOver)
    {
        SceneManager.LoadScene("GameOver");
    }



   

}
