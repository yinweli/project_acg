public enum EnemyType
{
    Enemy_001,
    Enemy_002,

    EnemyCount,
}

public enum WeaponType
{
    Weapon_null,
    Weapon_001, // 手電筒.
    Weapon_002, // 小刀
    Weapon_003, // 武士刀.
    Weapon_004, // 電鋸.
    Weapon_005, // 手槍.
    Weapon_006, // 左輪手槍.
    Weapon_007, // 沙漠之鷹.
    Weapon_008, // 衝鋒槍.
    Weapon_009, // 突擊步槍.
    Weapon_010, // 輕機槍.

    WeaponCount,
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
	Random_Currency, // 隨機增加通貨
	Random_Battery, // 隨機增加電池
	Random_LightAmmo, // 隨機增加輕型彈藥
	Random_HeavyAmmo, // 隨機增加重型彈藥
	Passive_StaminaLimit, // 被動增加耐力上限
	Passive_StaminaRecovery, // 被動增加耐力回復
	Passive_CriticalStrike, // 被動增加致命
	Passive_AddDamage, // 被動增加增傷
}

// 怪物模式列舉
public enum ENUM_ModeMonster
{
	Null = 0, 
	Normal, // 正常
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

// 角色外觀列舉.
public enum ENUM_Role
{
    HairA,
    HairB,    
    Adorn,    
    BrowR,
    BrowL,    
    EyeR,
    EyeL,
    EyeBgR,
    EyeBgL,    
    Mouth,
    Face_Bg,
    Face_BR,
    Face_BL,    
    Body,
    HandR,
    HandL,    
    FootR,
    FootL,

    Role_Count,
}