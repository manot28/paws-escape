using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField] private float windForce = 15f;

    [SerializeField]
    private Vector2 windDirection = Vector2.left;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb =
                collision.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.AddForce(windDirection.normalized * windForce);
        }
    }
}