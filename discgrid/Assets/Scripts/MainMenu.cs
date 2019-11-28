using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public void GetInput(string playerName)
	{
		if (!string.IsNullOrEmpty (playerName)) 
		{
			PlayerPrefs.SetString ("CurrentPlayer", playerName);
			StartGame ();
		}

	}
		
    // Start a new game
	public void StartGame()
	{
		// Load game scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	// Quit game
	public void QuitGame()
	{
		Application.Quit();
    }
}
