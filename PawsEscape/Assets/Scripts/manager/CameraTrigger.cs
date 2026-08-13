using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, -10f);
            Camera.main.transform.position = newPos;
        }
    }
}