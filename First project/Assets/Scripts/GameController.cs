using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public int waveNumber = 0;
	
	private bool gameOver;
	private bool restart;

	public int score;

	void Update ()
	{
		if (restart && Input.GetButton ("Fire1"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void Start ()
	{
		score = 0;

		scoreText = GameObject.FindWithTag("ScoreText").GetComponent<GUIText>();
		gameOverText = GameObject.FindWithTag("GameOverText").GetComponent<GUIText>();
		restartText = GameObject.FindWithTag("RestartText").GetComponent<GUIText>();

		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";

		UpdateScore ();

		if (Network.isServer)
		{
			StartCoroutine (SpawnWaves ());
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Network.Instantiate (hazard, spawnPosition, spawnRotation,0);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			waveNumber++;
			if (waveNumber > 0)
			{
				GameOver("You Win!");
			}

			if (gameOver)
			{
				restartText.text = "Tap on screen to restart level.";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver (string endText)
	{
		gameOverText.text = endText;
		gameOver = true;
	}

}