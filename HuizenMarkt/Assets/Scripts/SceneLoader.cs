using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Object scene;
    [SerializeField] float delay = 0;
    [SerializeField] bool OnStart;

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