using UnityEngine;
using UnityEngine.Audio;
public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private PuzzlePieceData itemData;
    [SerializeField]
    private Inventory inventory;

    private bool playerNear;

    [SerializeField] private GameObject paw;
    [SerializeField] private AudioClip clip;
    private AudioSource source;

    private void Start()
    {
        source = GameObject.Find("PlayerAudio").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(itemData);

            if(source !=null)
                source.PlayOneShot(clip);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            if(paw)
                paw.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            if(paw)
                paw.SetActive(false);
        }
    }
}