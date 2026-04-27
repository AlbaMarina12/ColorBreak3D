using UnityEngine;

public class ColorCollision : MonoBehaviour
{
    private Renderer playerRenderer;
    private string currentColor = "Blue";

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        SetColor("Blue");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ColorWall"))
        {
            string wallColor = GetWallColor(other.gameObject.name);
            MixColor(wallColor);

            DestroyAllWalls();
        }
    }

    string GetWallColor(string wallName)
    {
        if (wallName.Contains("Red")) return "Red";
        if (wallName.Contains("Yellow")) return "Yellow";
        if (wallName.Contains("Blue")) return "Blue";

        return "";
    }

    void MixColor(string newColor)
    {
        if (currentColor == newColor)
        {
            SetColor(currentColor);
            return;
        }

        if ((currentColor == "Blue" && newColor == "Yellow") ||
            (currentColor == "Yellow" && newColor == "Blue"))
        {
            SetColor("Green");
        }
        else if ((currentColor == "Blue" && newColor == "Red") ||
                 (currentColor == "Red" && newColor == "Blue"))
        {
            SetColor("Purple");
        }
        else if ((currentColor == "Red" && newColor == "Yellow") ||
                 (currentColor == "Yellow" && newColor == "Red"))
        {
            SetColor("Orange");
        }
        else
        {
            SetColor(newColor);
        }
    }

    void SetColor(string colorName)
    {
        currentColor = colorName;

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
            case "Green":
                playerRenderer.material.color = Color.green;
                break;
            case "Purple":
                playerRenderer.material.color = new Color(0.5f, 0f, 0.5f);
                break;
            case "Orange":
                playerRenderer.material.color = new Color(1f, 0.5f, 0f);
                break;
        }
    }

    void DestroyAllWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("ColorWall");

        foreach (GameObject wall in walls)
        {
            Destroy(wall);
        }
    }
}