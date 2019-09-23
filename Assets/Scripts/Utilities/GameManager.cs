using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject spawnerObj;
    private Spawner spawner;
    public GameObject gameOverUI;
    public GameObject directionalButtons;
    public TextMeshProUGUI scoreUI;

    private int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (score == value) return;
            score = value;

            UpdateLevel(score);

            OnScoreChange?.Invoke(score);
        }
    }

    [HideInInspector]
    public int level = 1;
    private bool addDifficulty = false;
    private int lastLevelThresholdScore = 0;
    private int diffScoreBetweenLevels = 200;

    
    private delegate void OnScoreChangeDelegate(int newVal);
    private event OnScoreChangeDelegate OnScoreChange;

    private void Start()
    {
        spawner = spawnerObj.GetComponent<Spawner>();
        spawner.DiffNbSpawn = 4;

        OnScoreChange += LevelChangeHandler;

        StartCoroutine(UpdateScore());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ShowGameOverScreen()
    {
        gameOverUI.SetActive(true);
        directionalButtons.SetActive(false);
        Destroy(spawner);
    }

    private IEnumerator UpdateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(.08f);
            scoreUI.text = Score.ToString();
        }
    }

    private void UpdateLevel(int score)
    {
        if (score - lastLevelThresholdScore > diffScoreBetweenLevels)
        {
            addDifficulty = true;
            lastLevelThresholdScore += diffScoreBetweenLevels;
        }
    }

    private void LevelChangeHandler(int scoreVal)
    {

        if (addDifficulty)
        {
            if (spawner.DiffNbSpawn > 0)
            {
                spawner.DiffNbSpawn--;
            }

            if (spawner.timeBtwSpawn - .5f > 0)
            {
                spawner.timeBtwSpawn -= .4f;
            }
            Collectible.fallingSpeed += 30f;
            addDifficulty = false;
        }
    }

}