using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceUI : MonoBehaviour
{
    [SerializeField]
    private string pieceID;

    [SerializeField]
    private Inventory inventory;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.enabled =
            inventory.HasPiece(pieceID);
    }
}