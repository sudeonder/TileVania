using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] float deathWait = 1.0f;
    void Awake() 
    {
        // find how many GameManagers in the scene

        int numOfGameManagers = FindObjectsOfType<GameManager>().Length;
        if (numOfGameManagers > 1)
        {
            Destroy(gameObject);

        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayerDeath()
    {
        if (playerLives > 1)
        {
            // load the current scene again
            StartCoroutine("TakeLife");
        }
        else{
            RestartGame();
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    IEnumerator TakeLife()
    {
        playerLives --;
        yield return new WaitForSeconds(deathWait);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
