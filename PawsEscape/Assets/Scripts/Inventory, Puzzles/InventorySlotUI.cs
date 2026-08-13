using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void SetItem(Sprite itemIcon)
    {
        icon.sprite = itemIcon;
        icon.enabled = true;
    }
}