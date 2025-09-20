using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "CraftTypeData", menuName = "Data/Craft Type Data")]
public class CraftTypeData : ScriptableObject
{
    public string craftTypeName;          // 加工类型名称
    public CraftType craftType;           // 枚举类型（用于兼容现有代码）
    public Sprite icon;                   // 图标
    public Color displayColor = Color.white; // 显示颜色
    public string description;            // 描述
    
    [Tooltip("加工所需时间（秒）")]
    public float processingTime = 2f;
}