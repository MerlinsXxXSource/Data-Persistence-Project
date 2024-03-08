using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text playerNameText;
    public Text ScoreText;
    public Text highscoreText;
    public GameObject GameOverText;
    public GameObject menuButton;
    public GameObject quitButton;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        // Get the player name
        string playerName = PlayerPrefs.GetString("PlayerName");

        // Update the player name text field
        playerNameText.text = playerName;

        // Get the high score for the player
        int highscore = PlayerPrefs.GetInt(playerName);

        // Update the high score text field
        highscoreText.text = "Highscore: " + highscore;

        // Create the bricks
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void UpdateHighScore(int score)
    {
        // Get the player name
        string playerName = PlayerPrefs.GetString("PlayerName");

        // Get the high score for the player
        int highscore = PlayerPrefs.GetInt(playerName);

        // If the new score is higher than the high score, update the high score
        if (score > highscore)
        {
            PlayerPrefs.SetInt(playerName, score);
            highscoreText.text = "Highscore: " + score;
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        quitButton.SetActive(true);
        menuButton.SetActive(true);
        // Update the highscore
        UpdateHighScore(m_Points);
        PlayerPrefs.Save();
    }

    public void OnMainMenu()
    {
        // Load the main menu
        SceneManager.LoadScene("StartMenu");
    }

    public void OnQuitButtonPress()
    {
        // Quit the game
        Application.Quit();

    }
}
