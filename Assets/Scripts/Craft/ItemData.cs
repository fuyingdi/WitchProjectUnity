using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType; // 例如：Material, Tool, Consumable
    public bool isStackable = true;
}

public enum ItemType { Material, Tool, Consumable }
