using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private int direction = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * direction * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction *= -1;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
