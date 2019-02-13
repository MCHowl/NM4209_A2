using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public Canvas startCanvas;
	public Canvas mainCanvas;
	public Canvas gameOverCanvas;
	public TMPro.TextMeshProUGUI scoreText;

	public GameObject playerPrefab;
	private PlayerController playerInstance;

	private bool isGame = false;

	public GameObject FoodPrefab;
	private Transform food_holder;

	private float spawnX = 8;
	private float spawnY = 4;

	private int foodCount = 3;
	private float foodSpawnFrequency = 5;

    void Start() {
		food_holder = new GameObject("Food").transform;

		mainCanvas.enabled = false;
		gameOverCanvas.enabled = false;
    }

    void Update() {
		if (isGame) {
			scoreText.text = playerInstance.score.ToString();

			if (playerInstance.health <= 0) {
				EndGame();
			}
		} else {
			if (startCanvas.enabled && Input.anyKeyDown) {
				StartGame();
			}

			if (gameOverCanvas.enabled && Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
    }

	void StartGame() {
		playerInstance = Instantiate(playerPrefab).GetComponent<PlayerController>();
		StartCoroutine(SpawnFood());

		mainCanvas.enabled = true;
		startCanvas.enabled = false;

		isGame = true;
	}

	void EndGame() {
		isGame = false;
		StopAllCoroutines();

		gameOverCanvas.enabled = true;
		Destroy(playerInstance.gameObject);
		Destroy(food_holder.gameObject);
	}

	IEnumerator SpawnFood() {
		while (true) {
			if (food_holder.childCount < foodCount) {
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnX, spawnX), Random.Range(-spawnY, spawnY), 0);

				Instantiate(FoodPrefab, spawnPosition, Quaternion.identity, food_holder);
			}

			yield return new WaitForSeconds(foodSpawnFrequency);
		}
	}
}
