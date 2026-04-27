using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float cameraHeight = 3f;
    public float cameraDistanceZ = -7f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Mantiene la cámara fija en altura (Y)
            transform.position = new Vector3(
                target.position.x,
                cameraHeight,
                target.position.z + cameraDistanceZ
            );

            // Hace que mire al jugador sin cambiar la altura
            Vector3 lookPosition = new Vector3(
                target.position.x,
                cameraHeight,
                target.position.z
            );

            transform.LookAt(lookPosition);
        }
    }
}