using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GoToTheNextScene : MonoBehaviour
{
    [SerializeField] private float time = 3f;
    [SerializeField] private string sceneName;

    private void Start()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(time);

        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadSceneAsync(sceneName);
        else
            Debug.LogError("Scene name empty");
    }
}