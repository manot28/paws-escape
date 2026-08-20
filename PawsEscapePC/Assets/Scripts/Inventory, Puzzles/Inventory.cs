using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private Transform inventoryParent;

    private List<PuzzlePieceData> items =
        new List<PuzzlePieceData>();

    public void AddItem(PuzzlePieceData itemData)
    {
        items.Add(itemData);

        GameObject slot = Instantiate(slotPrefab, inventoryParent);

        InventorySlotUI slotUI = slot.GetComponent<InventorySlotUI>();

        slotUI.SetItem(itemData.icon);
    }

    public bool HasPiece(string id)
    {
        foreach (PuzzlePieceData item in items)
        {
            if (item.id == id)
                return true;
        }
        return false;
    }

    public void RemovePiece(string id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == id)
            {
                items.RemoveAt(i);
                break;
            }
        }
    }

    public void RefreshUI()
    {
        foreach (Transform child in inventoryParent)
        {
            Destroy(child.gameObject);
        }

        foreach (PuzzlePieceData item in items)
        {
            GameObject slot =
                Instantiate(slotPrefab, inventoryParent);

            InventorySlotUI slotUI =
                slot.GetComponent<InventorySlotUI>();

            slotUI.SetItem(item.icon);
        }
    }
}