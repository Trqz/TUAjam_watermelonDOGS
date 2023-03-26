using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneInSeconds : MonoBehaviour
{
    [SerializeField]
    private float secondsToNextScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextSceneCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator NextSceneCountdown()
    {
        yield return new WaitForSeconds(secondsToNextScene);

        NextScene();

        yield return null;
    }

    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
