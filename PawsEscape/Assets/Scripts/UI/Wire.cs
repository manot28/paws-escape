using UnityEngine;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    public RectTransform canvasRect;
    public RectTransform startPoint;
    public RectTransform wireLine;

    [SerializeField] private RectTransform targetPoint;
    [SerializeField] private float snapDistance = 50f;

    private bool isDragging;
    public bool finishPos;

    public AudioManager audioUI;

    private void Update()
    {
        if (finishPos)
        {
            UpdateWire(
                startPoint.anchoredPosition,
                targetPoint.anchoredPosition
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (finishPos) return;

        isDragging = true;
        wireLine.gameObject.SetActive(true);

        audioUI.PlaySound("rope");

        UpdateWire(
            startPoint.anchoredPosition,
            GetLocalPosition(eventData.position)
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 localPosition = GetLocalPosition(eventData.position);

        UpdateWire(
            startPoint.anchoredPosition,
            localPosition
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;

        isDragging = false;

        Vector2 localPosition = GetLocalPosition(eventData.position);

        float distance = Vector2.Distance(localPosition, targetPoint.anchoredPosition);

        if (distance <= snapDistance)
        {
            finishPos = true;

            UpdateWire(
                startPoint.anchoredPosition,
                targetPoint.anchoredPosition
            );
        }
        else
        {
            wireLine.gameObject.SetActive(false);
        }
    }

    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            null,
            out Vector2 localPosition
        );

        return localPosition;
    }

    private void UpdateWire(Vector2 start, Vector2 end)
    {
        Vector2 dir = end - start;
        float length = dir.magnitude;

        wireLine.sizeDelta = new Vector2( length, wireLine.sizeDelta.y);

        wireLine.anchoredPosition = start + dir * 0.5f;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        wireLine.rotation = Quaternion.Euler(0,0,angle);
    }
}