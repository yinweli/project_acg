public enum ENUM_Language
{
    enUS,
    zhTW,

    LanguageCount,
}

public enum ENUM_Weapon
{
    Null,
    Light, // 手電筒.
    Knife, // 小刀
    Pistol, // 手槍.
    Revolver, // 左輪手槍.
    Eagle, // 沙漠之鷹.
    SUB, // 衝鋒槍.
    Rifle, // 突擊步槍.
    LMG, // 輕機槍.

    Count,
}

// 方向列舉
public enum ENUM_Dir
{
	Null = 0, 
	Up, // 上
	Left, // 左
	Right, // 右
	Down, // 下
}

// 地圖元件列舉
public enum ENUM_Map
{
	Null = 0, 
	MapObjt_0, 
	MapObjt_1, 
	MapObjt_2, 
	MapObjt_3, 
	MapObjt_4, 
	MapObjt_5, 
	MapObjt_6, 
	MapObjt_7, 
	MapObjt_8, 
	MapObjt_9, 
	MapObjt_10, 
	MapObjt_11, 
	MapStart, // 起點
	MapEnd, // 終點
	MapRoad, // 道路
	MapBase, // 底圖
}

// 裝備模式列舉
public enum ENUM_ModeEquip
{
	Null = 0, 
	Light, // 光源
	Damage, // 傷害
}

// 特性模式列舉
public enum ENUM_ModeFeature
{
	Null = 0, 
	StaminaLimit, // 增加耐力上限
	StaminaRecovery, // 增加耐力回復
	CriticalStrike, // 增加致命
	AddDamage, // 增加增傷
	AddLeastBomb, // 增加最少絕招次數
	AddShield, // 增加護盾次數
	AddEnegry, // 增加怪物能量
	Frozen, // 冰凍
}

// 怪物模式列舉
public enum ENUM_ModeMonster
{
	Null = 0, 
	Normal, // 正常
	ActiveDark, // 黑暗啟動
    Tied, // 綁人. 
	Boss, // 魔王
}

// 射程列舉
public enum ENUM_Range
{
	Null = 0, 
	Near, // 近
	Far, // 遠
}

// 資源列舉
public enum ENUM_Resource
{
	Null = 0, 
	Battery, // 電池
	LightAmmo, // 輕型彈藥
	HeavyAmmo, // 重型彈藥

    Resource_Count,
}

// 地圖拾取列舉
public enum ENUM_Pickup
{
	Member, // 成員
	Currency, // 通貨
	Battery, // 電池
	LightAmmo, // 輕型彈藥
	HeavyAmmo, // 重型彈藥
	Bomb, // 絕招
	Collection, // 收集物品
}

// 獎勵列舉
public enum ENUM_Reward
{
	Null = 0, 
	Upgrade, // 裝備升級
	Looks, // 角色外觀
}

// 條件列舉
public enum ENUM_Condition
{
	Null = 0, 
	Collection, // 收集物品
	Single_Stage, // 單輪通過關卡數量
	Single_SaveMember, // 單輪救援成員人數
	Single_LostMember, // 單輪失去成員人數
	Total_CurrencyPickup, // 累積拾取金幣數量
	Total_MonsterKill, // 累積擊敗怪物數量
}