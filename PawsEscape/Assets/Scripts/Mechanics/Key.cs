using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KeyPickup : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    [SerializeField] private TextMeshProUGUI keyText;
    private bool pickedUp = false;

    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private string textShown;
    private bool playerNearby;

    [SerializeField] private AudioClip pickUp;
    private AudioSource source;
    private void Start()
    {
        source = GameObject.Find("PlayerAudio").GetComponent<AudioSource>();
        keyText.text = "Keys: " + GameManager.Instance.Keys.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerNearby = true;
        myText.gameObject.SetActive(true);
        myText.text = textShown;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;

            myText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pickedUp) return;

        if (playerNearby && GameInput.Instance.InteractPressed)
        {
            pickedUp = true;

            source.PlayOneShot(pickUp);
            GameManager.Instance.AddKey(amount);
            keyText.text = "Keys: " + GameManager.Instance.Keys.ToString();

            Destroy(gameObject);
        }
    }
}