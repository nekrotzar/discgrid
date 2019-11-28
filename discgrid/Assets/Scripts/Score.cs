using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class AllScores 
{
	public List<PlayerScore> scores = new List<PlayerScore> ();

}

[System.Serializable]
public class PlayerScore
{
	public string playerName;
	public int playerScore;


}

public class Score : MonoBehaviour {

	public static int score;
	public Text scoreText;

	// Use this for initialization
	void Start ()
    {
		score = 0;
	}

	// Update is called once per frame
	void Update ()
    {
		//score++;
		//scoreText.text = "Score: " + score;
	}

	public int GetScore(){
		return score;
	}

    public void IncrementScoreWithKill()
    {
        score += 100;
        scoreText.text = "Score: " + score;
    }
}