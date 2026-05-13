using UnityEngine;

public class BarrierColor : MonoBehaviour
{
    public string colorName;

    private Renderer barrierRenderer;

    void Awake()
    {
        barrierRenderer = GetComponent<Renderer>();
    }

    public void SetColor(string newColor)
    {
        colorName = newColor;
        ApplyColor();
    }

    void ApplyColor()
    {
        if (barrierRenderer == null)
        {
            barrierRenderer = GetComponent<Renderer>();
        }

        switch (colorName)
        {
            case "Red":
                barrierRenderer.material.color = Color.red;
                break;

            case "Blue":
                barrierRenderer.material.color = Color.blue;
                break;

            case "Yellow":
                barrierRenderer.material.color = Color.yellow;
                break;

            case "White":
                barrierRenderer.material.color = Color.white;
                break;

            case "Black":
                barrierRenderer.material.color = Color.black;
                break;

            case "Green":
                barrierRenderer.material.color = Color.green;
                break;

            case "Purple":
                barrierRenderer.material.color = new Color(0.5f, 0f, 0.5f);
                break;

            case "Orange":
                barrierRenderer.material.color = new Color(1f, 0.5f, 0f);
                break;

            case "Pink":
                barrierRenderer.material.color = new Color(1f, 0.4f, 0.7f);
                break;

            case "Gray":
                barrierRenderer.material.color = Color.gray;
                break;

            case "Light Blue":
                barrierRenderer.material.color = new Color(0.4f, 0.8f, 1f);
                break;
        }
    }
}