using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartMenu() 
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().RestGame();
    }
    public void LoadGameOver() 
    {
        StartCoroutine(DieWithDelay());
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
    IEnumerator DieWithDelay() 
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game Over");
    }

}

