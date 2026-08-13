using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private bool isLockedDoor;
    [SerializeField] private string sceneToLoad;

    [Header("Save")]
    [SerializeField] private string doorId;

    [Header("Locks")]
    [SerializeField] private GameObject[] locks;

    [SerializeField] private TextMeshProUGUI keys;
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private bool playerNearby;
    private bool unlocked;

    private string SaveKey => "DOOR_" + doorId;

    private void Start()
    {
        unlocked = PlayerPrefs.GetInt(SaveKey, 0) == 1;

        UpdateLocksVisual();

        if (keys != null)
            keys.text = "Keys " + GameManager.Instance.Keys;
    }

    private void Update()
    {
        if (!playerNearby)
            return;

        if (GameInput.Instance.InteractPressed)
            Interact();
    }

    private void Interact()
    {
        // normal door
        if (!isLockedDoor)
        {
            SceneManager.LoadScene(sceneToLoad);
            return;
        }

        // already unlocked
        if (unlocked)
        {
            SceneManager.LoadScene(sceneToLoad);
            return;
        }

        int requiredKeys = locks.Length;

        bool paid = GameManager.Instance.UseKeys(requiredKeys);

        if (!paid)
        {
            Debug.Log("not enough keys");
            return;
        }

        unlocked = true;

        PlayerPrefs.SetInt(SaveKey, 1);
        PlayerPrefs.Save();

        source.PlayOneShot(clip);

        UpdateLocksVisual();

        if (keys != null)
            keys.text = "Keys " + GameManager.Instance.Keys;

        SceneManager.LoadScene(sceneToLoad);
    }

    private void UpdateLocksVisual()
    {
        bool showLocks = !unlocked;

        foreach (GameObject lockObj in locks)
            lockObj.SetActive(showLocks);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        myText.text = "Interact";
        myText.gameObject.SetActive(true);
        playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        myText.gameObject.SetActive(false);
        playerNearby = false;
    }
}