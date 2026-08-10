using UnityEngine;
using UnityEngine.EventSystems;

public class MovePuzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [SerializeField] private RectTransform form;

    [SerializeField] private float snapDistance = 50f;

    public bool finishPos;

    private AudioSource audioSource;
    [SerializeField] AudioClip snapClip;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();

        audioSource =
            GameObject.Find("AudioUI")
            .GetComponent<AudioSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (finishPos)
            return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float distance = Vector2.Distance(
            rectTransform.anchoredPosition,
            form.anchoredPosition
        );

        // snap into place if close enough 
        if (distance <= snapDistance)
        {
            audioSource.PlayOneShot(snapClip);
            rectTransform.anchoredPosition = form.anchoredPosition;
            finishPos = true;
        }
    }
}