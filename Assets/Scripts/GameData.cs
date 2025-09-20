using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/Game Data")]
public class GameData : ScriptableObject
{
    [ListDrawerSettings(HideAddButton = true)]
    [InlineEditor(InlineEditorModes.GUIOnly, DrawPreview = true, Expanded = true)][TableList(ShowIndexLabels = true, ShowPaging = true, NumberOfItemsPerPage = 5)]
    public List<ItemData> Items = new List<ItemData>();

    [Title("加工类型")]
    [TableList(ShowIndexLabels = true)]
    public List<CraftTypeData> CraftTypes = new List<CraftTypeData>();

    [PropertyOrder(1)]
    [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    private void ValidateData()
    {
        // 数据校验逻辑
    }
    [Button("Add Item")]
    public void NewItem()
    {
        var a = new ItemData();
        Items.Add(a);
        a.host = this.Items;
    }
    [Button("Add Craft Type")]
    public void NewCraftType()
    {
        var craftType = ScriptableObject.CreateInstance<CraftTypeData>();
        CraftTypes.Add(craftType);
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
