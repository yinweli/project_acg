public enum EnemyType
{
    Enemy_001,
    Enemy_002,

    EnemyCount,
}

public enum WeaponType
{
    Weapon_null,
    Weapon_Light,
    Weapon_001,
    Weapon_002,

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
	Random, // 隨機
	Passive, // 被動
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
}

// 效果列舉
public enum ENUM_Effect
{
	Null = 0, 
	Currency, // 通貨
	Pressure, // 壓力
	Stamina, // 耐力
	Battery, // 電池
	LightAmmo, // 輕型彈藥
	HeavyAmmo, // 重型彈藥
	Recovery, // 回復
	CriticalStrike, // 致命
	Damage, // 增傷
}