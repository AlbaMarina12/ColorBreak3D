using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Choqué con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El jugador tocó la barra: " + gameObject.tag);
            levelGenerator.CheckWallCollision(gameObject.tag);
        }
    }
}