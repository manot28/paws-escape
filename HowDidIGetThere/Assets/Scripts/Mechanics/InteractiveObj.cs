using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveObj : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private string textShown;

    [SerializeField] private bool turnOff;
    [SerializeField] private GameObject Spark;
    private bool playerNearby;
    private void Start()
    {
        myText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby && GameInput.Instance.InteractPressed)
        {
            if (menu != null)
            {
                menu.SetActive(true);
                myText.gameObject.SetActive(false);
            }
            if (turnOff)
            {
                enabled = false;
                BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
                bc2d.enabled = false;
                Destroy(Spark);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;

            myText.gameObject.SetActive(true);

            myText.text = textShown;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;

            myText.gameObject.SetActive(false);
        }
    }
}