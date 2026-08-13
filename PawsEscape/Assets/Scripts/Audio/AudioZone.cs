using UnityEngine;

public class AudioZone : MonoBehaviour
{
    // FADE SOUND IF FAR AWAY FROM THE PLAYER
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float targetVolume = 1f;
    [SerializeField]
    private float fadeSpeed = 2f;

    private bool playerInside;

    private void Update()
    {
        float desiredVolume =
            playerInside?targetVolume:0f;

        audioSource.volume =
            Mathf.Lerp(
                audioSource.volume,
                desiredVolume,
                fadeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
    }
}