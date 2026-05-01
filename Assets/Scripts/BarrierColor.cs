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
        }
    }
}