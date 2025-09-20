/// <summary>
/// 加工类型枚举，用于向后兼容
/// 0: 切割 (Chop)
/// 1: 研磨 (Grind)
/// 2: 烘烤 (Roast)
/// 3: 煮沸 (Boil)
/// </summary>
public enum CraftType
{
    Chop,    // 对应 ChopCraftType.asset
    Grind,   // 对应 GrindCraftType.asset
    Roast,   // 对应 RoastCraftType.asset
    Boil,    // 对应 BoilCraftType.asset
}