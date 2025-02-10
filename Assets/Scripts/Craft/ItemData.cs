using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType; // 例如：Material, Tool, Consumable
    public bool isStackable = true;

    [HideInInspector] public List<ItemData> host;
    [Button("X")]
    public void Remove()
    {
        host.Remove(this);
    }
}

public enum ItemType { Material, Tool, Consumable }
