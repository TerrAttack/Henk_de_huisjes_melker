using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Object scene;
    [SerializeField] float delay = 0;
    [SerializeField] bool OnStart = false;

    public void Start()
    {
        if (OnStart) StartCoroutine(loadSceneDelay());
    }

    public void LoadScene()
    {
        StartCoroutine(loadSceneDelay());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator loadSceneDelay()
    {
        yield return new WaitForSeconds(delay);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}