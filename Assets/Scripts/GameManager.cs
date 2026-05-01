using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public Renderer playerRenderer;
    public Transform player;

    [Header("Barreras")]
    public BarrierColor[] barriers;
    public float distanceBetweenLevels = 15f;

    [Header("UI")]
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;

    private int currentLevel = 0;
    private int score = 0;
    private int highScore = 0;

    private string correctBarrierColor;
    private LevelData[] levels;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        CreateLevels();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        LoadLevel();
    }

    void CreateLevels()
    {
        levels = new LevelData[]
        {
            new LevelData("Blue", "Yellow", "Green", new string[] { "Yellow", "Red", "Blue" }),
            new LevelData("Red", "Blue", "Purple", new string[] { "Blue", "Yellow", "White" }),
            new LevelData("Yellow", "Red", "Orange", new string[] { "Red", "Blue", "Black" }),
            new LevelData("Red", "White", "Pink", new string[] { "White", "Blue", "Yellow" }),
            new LevelData("Black", "White", "Gray", new string[] { "White", "Red", "Blue" }),

            new LevelData("Blue", "Yellow", "Green", new string[] { "Red", "Yellow", "White" }),
            new LevelData("Red", "Blue", "Purple", new string[] { "Yellow", "Black", "Blue" }),
            new LevelData("Yellow", "Red", "Orange", new string[] { "Blue", "Red", "White" }),
            new LevelData("White", "Red", "Pink", new string[] { "Red", "Blue", "Black" }),
            new LevelData("White", "Black", "Gray", new string[] { "Yellow", "Black", "Blue" }),

            new LevelData("Blue", "White", "Light Blue", new string[] { "White", "Red", "Yellow" }),
            new LevelData("Red", "Yellow", "Orange", new string[] { "Blue", "Yellow", "Black" }),
            new LevelData("Yellow", "Blue", "Green", new string[] { "Red", "Blue", "White" }),
            new LevelData("Blue", "Red", "Purple", new string[] { "Yellow", "Black", "Red" }),
            new LevelData("Black", "White", "Gray", new string[] { "Blue", "White", "Red" }),

            new LevelData("Red", "White", "Pink", new string[] { "Yellow", "White", "Blue" }),
            new LevelData("Blue", "Yellow", "Green", new string[] { "Black", "Yellow", "Red" }),
            new LevelData("Yellow", "Red", "Orange", new string[] { "White", "Blue", "Red" }),
            new LevelData("White", "Blue", "Light Blue", new string[] { "Blue", "Red", "Black" }),
            new LevelData("Red", "Blue", "Purple", new string[] { "White", "Blue", "Yellow" })
        };
    }

    void LoadLevel()
    {
        if (currentLevel >= levels.Length)
        {
            WinGame();
            return;
        }

        LevelData level = levels[currentLevel];

        correctBarrierColor = level.correctBarrierColor;

        ApplyPlayerColor(level.playerColor);

        float newZ = player.position.z + distanceBetweenLevels;

        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].gameObject.SetActive(true);
            barriers[i].SetColor(level.barrierColors[i]);

            Vector3 position = barriers[i].transform.position;
            position.z = newZ;
            barriers[i].transform.position = position;
        }

        UpdateUI(level.targetColor);
    }

    public void CheckAnswer(string selectedBarrierColor)
    {
        if (selectedBarrierColor == correctBarrierColor)
        {
            score += 10;
            currentLevel++;

            SaveHighScore();

            HideBarriers();
            LoadLevel();
        }
        else
        {
            GameOver();
        }
    }

    void HideBarriers()
    {
        foreach (BarrierColor barrier in barriers)
        {
            barrier.gameObject.SetActive(false);
        }
    }

    void UpdateUI(string targetColor)
    {
        instructionText.text = "Forma el color: " + targetColor;
        scoreText.text = "Score: " + score;
        levelText.text = "Nivel: " + (currentLevel + 1);
        highScoreText.text = "High Score: " + highScore;
    }

    void GameOver()
    {
        SaveHighScore();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    void WinGame()
    {
        instructionText.text = "¡Ganaste los 20 niveles!";
        SaveHighScore();
        Time.timeScale = 0f;
    }

    void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    void ApplyPlayerColor(string colorName)
    {
        switch (colorName)
        {
            case "Red":
                playerRenderer.material.color = Color.red;
                break;

            case "Blue":
                playerRenderer.material.color = Color.blue;
                break;

            case "Yellow":
                playerRenderer.material.color = Color.yellow;
                break;

            case "White":
                playerRenderer.material.color = Color.white;
                break;

            case "Black":
                playerRenderer.material.color = Color.black;
                break;
        }
    }
}

[System.Serializable]
public class LevelData
{
    public string playerColor;
    public string correctBarrierColor;
    public string targetColor;
    public string[] barrierColors;

    public LevelData(string playerColor, string correctBarrierColor, string targetColor, string[] barrierColors)
    {
        this.playerColor = playerColor;
        this.correctBarrierColor = correctBarrierColor;
        this.targetColor = targetColor;
        this.barrierColors = barrierColors;
    }
}