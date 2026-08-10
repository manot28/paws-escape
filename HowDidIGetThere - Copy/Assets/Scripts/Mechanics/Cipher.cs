using UnityEngine;
using TMPro;

public class Cipher : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject puzzle;
    [SerializeField] private TextMeshProUGUI myText;

    [SerializeField] private Inventory inventory;

    [SerializeField] private string requiredID;

    [SerializeField] private string inputText;

    [SerializeField] private string allowedText;

    [SerializeField] private string deniedText;

    [SerializeField] private Sprite openSprite;

    [SerializeField] private TMP_InputField inputField;

    private bool playerNearby;
    private SpriteRenderer sRenderer;
    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        myText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby && GameInput.Instance.InteractPressed)
        {
            if (inventory.HasPiece(requiredID))
                inputField.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enabled)
        {
            playerNearby = true;
            myText.gameObject.SetActive(true);

            if (inventory.HasPiece(requiredID))
                myText.text = allowedText;
            else
                myText.text = deniedText;
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

    public void CheckCode()
    {
        if (inputField.text == inputText)
        {
            sRenderer.sprite = openSprite;

            puzzle.SetActive(true);

            menu.SetActive(true);

            inputField.gameObject.SetActive(false);

            inventory.RemovePiece(requiredID);

            inventory.RefreshUI();

            enabled = false;

            Debug.Log("correct code");
        }
        else
            Debug.Log("wrong code");
    }

}