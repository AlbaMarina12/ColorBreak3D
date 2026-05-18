using UnityEngine;
using System.Collections;

public class SuperSpeedEffect : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float superSpeed = 12f;
    public float duration = 3f;

    public TrailRenderer trail;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        if (trail != null)
        {
            trail.emitting = false;
        }
    }

    public void ActivateSpeed()
    {
        StartCoroutine(SpeedBoost());
    }

    IEnumerator SpeedBoost()
    {
        playerMovement.speed = superSpeed;

        if (trail != null)
        {
            trail.emitting = true;
        }

        yield return new WaitForSeconds(duration);

        playerMovement.speed = normalSpeed;

        if (trail != null)
        {
            trail.emitting = false;
        }
    }
}