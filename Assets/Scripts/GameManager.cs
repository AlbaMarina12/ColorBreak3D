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

    private string currentPlayerColor;
    private string targetColor;
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
            new LevelData("Blue", "Green", new string[] { "Yellow", "Red", "Blue" }),
            new LevelData("Red", "Purple", new string[] { "Blue", "Yellow", "White" }),
            new LevelData("Yellow", "Orange", new string[] { "Red", "Blue", "Black" }),
            new LevelData("Red", "Pink", new string[] { "White", "Blue", "Yellow" }),
            new LevelData("Black", "Gray", new string[] { "White", "Red", "Blue" }),

            new LevelData("Blue", "Green", new string[] { "Red", "Yellow", "White" }),
            new LevelData("Red", "Purple", new string[] { "Yellow", "Black", "Blue" }),
            new LevelData("Yellow", "Orange", new string[] { "Blue", "Red", "White" }),
            new LevelData("White", "Pink", new string[] { "Red", "Blue", "Black" }),
            new LevelData("White", "Gray", new string[] { "Yellow", "Black", "Blue" }),

            new LevelData("Blue", "Light Blue", new string[] { "White", "Red", "Yellow" }),
            new LevelData("Red", "Orange", new string[] { "Blue", "Yellow", "Black" }),
            new LevelData("Yellow", "Green", new string[] { "Red", "Blue", "White" }),
            new LevelData("Blue", "Purple", new string[] { "Yellow", "Black", "Red" }),
            new LevelData("Black", "Gray", new string[] { "Blue", "White", "Red" }),

            new LevelData("Red", "Pink", new string[] { "Yellow", "White", "Blue" }),
            new LevelData("Blue", "Green", new string[] { "Black", "Yellow", "Red" }),
            new LevelData("Yellow", "Orange", new string[] { "White", "Blue", "Red" }),
            new LevelData("White", "Light Blue", new string[] { "Blue", "Red", "Black" }),
            new LevelData("Red", "Purple", new string[] { "White", "Blue", "Yellow" })
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

        currentPlayerColor = level.playerColor;
        targetColor = level.targetColor;

        ApplyPlayerColor(currentPlayerColor);

        float newZ = player.position.z + distanceBetweenLevels;

        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].gameObject.SetActive(true);
            barriers[i].SetColor(level.barrierColors[i]);

            Vector3 position = barriers[i].transform.position;
            position.z = newZ;
            barriers[i].transform.position = position;
        }

        UpdateUI(targetColor);
    }

    public void CheckAnswer(string selectedBarrierColor)
    {
        string resultColor = MixColors(currentPlayerColor, selectedBarrierColor);

        if (resultColor == targetColor)
        {
            score += 10;
            currentPlayerColor = resultColor;
            ApplyPlayerColor(currentPlayerColor);

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

    string MixColors(string playerColor, string barrierColor)
    {
        if ((playerColor == "Blue" && barrierColor == "Yellow") ||
            (playerColor == "Yellow" && barrierColor == "Blue"))
            return "Green";

        if ((playerColor == "Red" && barrierColor == "Blue") ||
            (playerColor == "Blue" && barrierColor == "Red"))
            return "Purple";

        if ((playerColor == "Yellow" && barrierColor == "Red") ||
            (playerColor == "Red" && barrierColor == "Yellow"))
            return "Orange";

        if ((playerColor == "Red" && barrierColor == "White") ||
            (playerColor == "White" && barrierColor == "Red"))
            return "Pink";

        if ((playerColor == "Black" && barrierColor == "White") ||
            (playerColor == "White" && barrierColor == "Black"))
            return "Gray";

        if ((playerColor == "Blue" && barrierColor == "White") ||
            (playerColor == "White" && barrierColor == "Blue"))
            return "Light Blue";

        return "Wrong";
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

            case "Green":
                playerRenderer.material.color = Color.green;
                break;

            case "Purple":
                playerRenderer.material.color = new Color(0.5f, 0f, 0.5f);
                break;

            case "Orange":
                playerRenderer.material.color = new Color(1f, 0.5f, 0f);
                break;

            case "Pink":
                playerRenderer.material.color = new Color(1f, 0.4f, 0.7f);
                break;

            case "Gray":
                playerRenderer.material.color = Color.gray;
                break;

            case "Light Blue":
                playerRenderer.material.color = new Color(0.4f, 0.8f, 1f);
                break;
        }
    }
}

[System.Serializable]
public class LevelData
{
    public string playerColor;
    public string targetColor;
    public string[] barrierColors;

    public LevelData(string playerColor, string targetColor, string[] barrierColors)
    {
        this.playerColor = playerColor;
        this.targetColor = targetColor;
        this.barrierColors = barrierColors;
    }
}