using UnityEngine;
using System.Collections.Generic;
public class SwitchWind : MonoBehaviour
{
    public bool playerNearby;
    [SerializeField] private GameObject wind;

    [SerializeField] private Animator anim;
    [SerializeField] private List<Sprite> sprites;
    private bool isOn;

    [SerializeField] private AudioSource switchSource;
    [SerializeField] private AudioClip switchClip;
    private SpriteRenderer sRenderer;
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        playerNearby = false;
        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNearby && GameInput.Instance.InteractPressed)
        {
            if(!switchSource.isPlaying)
                switchSource.PlayOneShot(switchClip);
            wind.SetActive(!wind.activeSelf);
            Debug.Log("wind" + wind.activeSelf);
            isOn = !isOn;
            anim.enabled = isOn;
        }
        if (isOn)
            sRenderer.sprite = sprites[0];
        else 
            sRenderer.sprite = sprites[1];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerNearby=true;    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerNearby = false;
    }
}
