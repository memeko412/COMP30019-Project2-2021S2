using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNextLevel()
    {
        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        transition.SetTrigger("Start");
    }

    public void Tstart()
    {
        transition.SetTrigger("Start");
    }
    public void Tend()
    {
        transition.SetTrigger("End");
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
}
