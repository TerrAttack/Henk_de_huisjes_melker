using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Object scene;
    [SerializeField] float delay = 0;

    public void loadScene()
    {
        StartCoroutine(loadSceneDelay());
    }

    IEnumerator loadSceneDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene.name); 
    }
}