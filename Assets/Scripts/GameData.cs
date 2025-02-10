using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/Game Data")]
public class GameData : ScriptableObject
{
    public ItemData Item;
    [ListDrawerSettings(HideAddButton = true)]
    [InlineEditor(InlineEditorModes.GUIOnly, DrawPreview = true, Expanded = true)][TableList(ShowIndexLabels = true, ShowPaging = true, NumberOfItemsPerPage = 5)]
    public List<ItemData> Items = new List<ItemData>();

    [PropertyOrder(1)]
    [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void ValidateData()
    {
        // 数据校验逻辑
    }
    [Button("Add")]
    public void NewItem()
    {
        var a = new ItemData();
        Items.Add(a);
        a.host = this.Items;
    }
    [Button("Remove")]
    public void Remove()
    {
        Items.Clear();
    }

    [PropertyOrder(1)]
    [Title("合成配方")]
    [TableList(ShowIndexLabels = true)]
    public List<CraftingRecipe> Recipes = new List<CraftingRecipe>();

}


[System.Serializable]
public class CraftingRecipe
{
    [TableColumnWidth(70)]
    public int OutputItemID;

    [TableList(DrawScrollView = false)]
    public List<Ingredient> Inputs;
}
[System.Serializable]
public class Ingredient
{
    public int ItemID;   // 需要的物品ID（例如：1=木材，2=铁矿）
    public int Quantity; // 需要的数量
}
