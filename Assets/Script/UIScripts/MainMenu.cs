using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Animator animator;

    public void StartAnimation()
    {
        animator.SetTrigger("FadeOut");
    }

    public void PlayGame()
    {
        Application.LoadLevel("Scene1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
