using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    [Header("Jugador")]
    public GameObject player;
    public Renderer playerRenderer;

    [Header("Prefabs de barreras")]
    public GameObject redWallPrefab;
    public GameObject yellowWallPrefab;
    public GameObject blueWallPrefab;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI targetColorText;
    public GameObject gameOverPanel;

    [Header("Configuración")]
    public float wallYPosition = 1f;
    public float wallZPosition = 8f;
    public float leftX = -4f;
    public float centerX = 0f;
    public float rightX = 4f;

    private GameObject[] currentWalls = new GameObject[3];

    private string playerColor = "Blue";
    private string correctWallColor;
    private string targetResultColor;

    private int score = 0;

    void Start()
    {
        Time.timeScale = 1f;

        score = 0;
        playerColor = "Blue";
        SetPlayerColor(playerColor);
        UpdateScore();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        GenerateNewLevel();
    }

    public void GenerateNewLevel()
    {
        DestroyCurrentWalls();

        ChooseColorChallenge();

        targetColorText.text = "Resultado objetivo: " + TranslateColor(targetResultColor);

        currentWalls[0] = Instantiate(redWallPrefab, new Vector3(leftX, wallYPosition, wallZPosition), Quaternion.identity);
        currentWalls[1] = Instantiate(yellowWallPrefab, new Vector3(centerX, wallYPosition, wallZPosition), Quaternion.identity);
        currentWalls[2] = Instantiate(blueWallPrefab, new Vector3(rightX, wallYPosition, wallZPosition), Quaternion.identity);

        currentWalls[0].tag = "Red";
        currentWalls[1].tag = "Yellow";
        currentWalls[2].tag = "Blue";
    }

    void ChooseColorChallenge()
    {
        string[] wallColors = { "Red", "Yellow", "Blue" };

        correctWallColor = wallColors[Random.Range(0, wallColors.Length)];
        targetResultColor = MixColors(playerColor, correctWallColor);
    }

    public void CheckWallCollision(string wallColor)
    {
        string resultColor = MixColors(playerColor, wallColor);

        if (wallColor == correctWallColor)
        {
            score++;
            UpdateScore();

            playerColor = resultColor;
            SetPlayerColor(playerColor);

            GenerateNewLevel();
        }
        else
        {
            GameOver();
        }
    }

    string MixColors(string colorA, string colorB)
    {
        if (colorA == colorB)
        {
            return colorA;
        }

        if ((colorA == "Blue" && colorB == "Yellow") || (colorA == "Yellow" && colorB == "Blue"))
        {
            return "Green";
        }

        if ((colorA == "Red" && colorB == "Yellow") || (colorA == "Yellow" && colorB == "Red"))
        {
            return "Orange";
        }

        if ((colorA == "Red" && colorB == "Blue") || (colorA == "Blue" && colorB == "Red"))
        {
            return "Purple";
        }

        if (colorA == "Green" && colorB == "Red")
        {
            return "Brown";
        }

        if (colorA == "Orange" && colorB == "Blue")
        {
            return "Brown";
        }

        if (colorA == "Purple" && colorB == "Yellow")
        {
            return "Brown";
        }

        return colorB;
    }

    void SetPlayerColor(string color)
    {
        if (playerRenderer == null) return;

        if (color == "Red")
            playerRenderer.material.color = Color.red;
        else if (color == "Yellow")
            playerRenderer.material.color = Color.yellow;
        else if (color == "Blue")
            playerRenderer.material.color = Color.blue;
        else if (color == "Green")
            playerRenderer.material.color = Color.green;
        else if (color == "Orange")
            playerRenderer.material.color = new Color(1f, 0.5f, 0f);
        else if (color == "Purple")
            playerRenderer.material.color = new Color(0.5f, 0f, 1f);
        else if (color == "Brown")
            playerRenderer.material.color = new Color(0.45f, 0.25f, 0.1f);
    }

    void DestroyCurrentWalls()
    {
        for (int i = 0; i < currentWalls.Length; i++)
        {
            if (currentWalls[i] != null)
            {
                Destroy(currentWalls[i]);
            }
        }
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void GameOver()
    {
        DestroyCurrentWalls();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    string TranslateColor(string color)
    {
        if (color == "Red") return "Rojo";
        if (color == "Yellow") return "Amarillo";
        if (color == "Blue") return "Azul";
        if (color == "Green") return "Verde";
        if (color == "Orange") return "Naranja";
        if (color == "Purple") return "Morado";
        if (color == "Brown") return "Café";

        return color;
    }
}