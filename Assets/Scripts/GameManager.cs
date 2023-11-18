using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
	public GameObject cloudPrefab;
	public GameObject gameOverSet;
	public GameObject[] thingsThatSpawn;
    public int score;
    public int cloudsMove;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
	public TextMeshProUGUI powerupText;
	private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
		Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemyOne", 1.0f, 4.0f);
        InvokeRepeating("CreateEnemyTwo", 1.0f, 5.0f);
		InvokeRepeating("SpawnSomething", 5f, 10f);

		cloudsMove = 1;
        score = 0;
		isGameOver = false;
		
        scoreText.text = "Score: " + score;
		livesText.text = "Lives: 3";
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadScene("Game");
        }
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
    }

	
	void CreateSky()
    {
        for (int i = 0; i < 50; i++) 
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-11f, 11f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
        }
    }
	
	void SpawnSomething()
    {
        int tempInt;
        tempInt = Random.Range(0, 3);
        Instantiate(thingsThatSpawn[tempInt], new Vector3(Random.Range(-7f, 7f), Random.Range(0, -5f), 0), Quaternion.identity);
    }
	
	public void GameOver()
    {
        CancelInvoke();
        cloudsMove = 0;
		GetComponent<AudioSource>().Stop();
        gameOverSet.SetActive(true);
        isGameOver = true;
    }

    public void EarnScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
        scoreText.text = "Score: " + score;
    }
	
	public void LivesChange(int currentLife)
	{
		livesText.text = "Lives: " + currentLife;
	}
	
	public void PowerupChange(string whatPowerup)
    {
        powerupText.text = whatPowerup;
    }
}