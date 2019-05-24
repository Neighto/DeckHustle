using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	
    public void StartGameNormal()
    {
        GameObject.FindGameObjectWithTag("Difficulty").GetComponent<DifficultyScript>().EnterNormalMode();
        SceneManager.LoadScene("Game");
    }

    public void StartGameLegendary()
    {
        GameObject.FindGameObjectWithTag("Difficulty").GetComponent<DifficultyScript>().EnterLegendaryMode();
        SceneManager.LoadScene("Game");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("BGMusic"));
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
