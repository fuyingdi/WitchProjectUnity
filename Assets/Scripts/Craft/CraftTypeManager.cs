using System.Collections.Generic;
using UnityEngine;

public class CraftTypeManager : MonoBehaviour
{
    public static CraftTypeManager Instance;

    [Tooltip("所有加工类型数据")]
    public List<CraftTypeData> CraftTypeList;

    private Dictionary<CraftType, CraftTypeData> _craftTypeDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            InitializeCraftTypes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeCraftTypes()
    {
        _craftTypeDict = new Dictionary<CraftType, CraftTypeData>();
        
        foreach (var craftTypeData in CraftTypeList)
        {
            // 建立枚举到数据的映射
            if (!_craftTypeDict.ContainsKey(craftTypeData.craftType))
            {
                _craftTypeDict.Add(craftTypeData.craftType, craftTypeData);
            }
        }
    }

    /// <summary>
    /// 根据枚举获取加工类型数据
    /// </summary>
    public CraftTypeData GetCraftTypeData(CraftType craftType)
    {
        if (_craftTypeDict != null && _craftTypeDict.ContainsKey(craftType))
        {
            return _craftTypeDict[craftType];
        }
        return null;
    }

    /// <summary>
    /// 根据枚举获取加工时间
    /// </summary>
    public float GetProcessingTime(CraftType craftType)
    {
        var data = GetCraftTypeData(craftType);
        return data != null ? data.processingTime : 2f; // 默认2秒
    }
}