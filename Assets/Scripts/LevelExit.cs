using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    // on collison with player
    // load next level
    float wait = 1.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine("NextScene");
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(wait);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        
        SceneManager.LoadScene(nextSceneIndex);
    }
}
