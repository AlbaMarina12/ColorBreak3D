using UnityEngine;

public class ColorCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        BarrierColor barrier = collision.gameObject.GetComponent<BarrierColor>();

        if (barrier != null)
        {
            GameManager.Instance.CheckAnswer(barrier.colorName);
        }
    }
}