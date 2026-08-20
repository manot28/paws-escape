using UnityEngine;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
            UpdateWire(startPoint.anchoredPosition, targetPoint.anchoredPosition);
            return;
        }

        if (!isDragging) return;

        Vector2 mousePos = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mousePos,
            null,
            out Vector2 localMousePos
        );

        UpdateWire(startPoint.anchoredPosition, localMousePos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (finishPos) return;

        isDragging = true;
        wireLine.gameObject.SetActive(true);
        audioUI.PlaySound("rope");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;

        isDragging = false;

        Vector2 mousePos = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mousePos,
            null,
            out Vector2 localMousePos
        );

        float distance = Vector2.Distance(
            localMousePos,
            targetPoint.anchoredPosition
        );

        if (distance <= snapDistance)
        {
            finishPos = true;

            UpdateWire(
                startPoint.anchoredPosition,
                targetPoint.anchoredPosition
            );
        }
        else
            wireLine.gameObject.SetActive(false);
    }

    private void UpdateWire(Vector2 start, Vector2 end)
    {
        Vector2 dir = end - start;
        // length of the wire (magnitude - length of the vector)
        float length = dir.magnitude;

        wireLine.sizeDelta = new Vector2(length, wireLine.sizeDelta.y);
        // center the wire between start and target pos
        wireLine.anchoredPosition = start + dir * 0.5f;

        // calculates and rotates the wire from start to mouse pos
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        wireLine.rotation = Quaternion.Euler(0, 0, angle);
    }
}