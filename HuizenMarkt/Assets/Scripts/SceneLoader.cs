using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Object scene = null;
    [SerializeField] float delay = 0;
    [SerializeField] bool OnStart = false;

    public void Start()
    {
        if(OnStart) StartCoroutine(loadSceneDelay());
    }

    public void LoadScene()
    {
        StartCoroutine(loadSceneDelay());
    }

    IEnumerator loadSceneDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene.name); 
    }
}