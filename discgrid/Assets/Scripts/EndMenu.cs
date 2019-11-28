using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndMenu : MonoBehaviour {

	public Text scoreText;
	public Text message;

	public GameObject highScores;
	public HighScoresManager manager;

	public void ReturnToMainMenu(){
		SceneManager.LoadScene(0);
	}

	public void Start(){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		DisplayScore ();
		DisplayMessage ();
	}

	public void DisplayScore()
	{
		scoreText.text = "Your score: " + PlayerPrefs.GetInt ("CurrentScore",0);
	}

	public void DisplayMessage ()
	{
		string m = PlayerPrefs.GetString ("GameStatus","Test");

		if (m == "DEAD") {
			message.text = "Game over!";
		} else if (m == "COMPLETE") 
		{
			message.text = "Level complete!";
		}



	}

}
