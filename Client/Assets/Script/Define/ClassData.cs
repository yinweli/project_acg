﻿using UnityEngine;
using LibCSNStandard;
using System;
using System.Collections;
using System.Collections.Generic;

// 地圖座標類別
public class MapCoor
{
	public int X = 0; // X軸座標
	public int Y = 0; // Y軸座標
	
	public MapCoor() {}
	public MapCoor(int x, int y)
	{
		X = x;
		Y = y;
	}
	public MapCoor Add(ENUM_Dir emDir, int iInterval)
	{
		MapCoor Result = new MapCoor(X, Y);
		
		switch(emDir)
		{
		case ENUM_Dir.Up: Result.Y += iInterval; break;
		case ENUM_Dir.Left: Result.X -= iInterval; break;
		case ENUM_Dir.Right: Result.X += iInterval; break;
		case ENUM_Dir.Down: Result.Y -= iInterval; break;
		default: break;
		}//switch
		
		return Result;
	}
	public Vector2 ToVector2()
	{
		return new Vector2(X * GameDefine.iBlockSize, Y * GameDefine.iBlockSize);
	}
}

// 地圖物件類別
public class MapObjt
{
	/* [Save] */ public MapCoor Pos = new MapCoor(); // 地圖座標
	/* [Save] */ public int Type = 0; // 物件型態
	/* [Save] */ public int Width = 0; // 物件寬度
	/* [Save] */ public int Height = 0; // 物件高度
	
	public bool Cover(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
	{
		if(x1 >= x2 && x1 >= x2 + w2)
			return false;
		else if(x1 <= x2 && x1 + w1 <= x2)
			return false;
		else if(y1 >= y2 && y1 >= y2 + h2)
			return false;
		else if(y1 <= y2 && y1 + h1 <= y2)
			return false;
		
		return true;
	}
	public bool Cover(MapCoor Data, int W, int H)
	{
		return Cover(Pos.X, Pos.Y, Width, Height, Data.X, Data.Y, W, H);
	}
	public bool Cover(MapCoor Data)
	{
		return Cover(Data, 1, 1);
	}
	public bool Cover(MapObjt Data)
	{
		return Cover(Data.Pos, Data.Width, Data.Height);
	}
}

public class Pickup
{
	/* [Save] */ public MapCoor Pos = new MapCoor(); // 地圖座標
	/* [Save] */ public int iType = 0; // 拾取列舉
	/* [Save] */ public int iCount = 0; // 數量
	/* [Save] */ public int iLooks = 0; // 外觀編號
	/* [Save] */ public bool bPickup = false; // 拾取旗標
}

public class Member
{
	/* [Save] */ public int iLooks = 0; // 外觀編號
	/* [Save] */ public int iEquip = 0; // 裝備編號
	/* [Save] */ public int iLiveStage = 0; // 生存關卡數
	/* [Save] */ public int iShield = 0; // 護盾次數
    /* [Save] */ public float fReactTime = 0; // 反應時間.
	/* [Save] */ public List<int> Feature = new List<int>(); // 特性列表
	/* [    ] */ public int iInvincibleTime = 0; // 無敵時間
	/* [    ] */ public float fCriticalStrike = 0.0f; // 致命
	/* [    ] */ public int iAddDamage = 0; // 增傷
}

public class Collection
{
	public ENUM_Weapon Weapon = ENUM_Weapon.Null; // 武器列舉
	public int iLevel = 0; // 武器等級
	public int iIndex = 0; // 索引編號

	public Collection(string data)
	{
		string[] szTemp = data.Split(new char[] { '_' });

		if(szTemp.Length >= 3)
		{
			Weapon = (ENUM_Weapon)System.Convert.ToInt32(szTemp[0]);
			iLevel = System.Convert.ToInt32(szTemp[1]);
			iIndex = System.Convert.ToInt32(szTemp[2]);
		}//if
	}
	public Collection(ENUM_Weapon weapon, int lv, int id)
	{
		Weapon = weapon;
		iLevel = lv;
		iIndex = id;
	}
	public string ToStringData()
	{
		return (int)Weapon + "_" + iLevel + "_" + iIndex;
	}
}

public class SaveMember
{
	public int iLooks = 0; // 外觀編號
	public int iEquip = 0; // 裝備編號
	public int iLiveStage = 0; // 生存關卡數
	public int iShield = 0; // 護盾次數
    public float fReactTime = 0; // 角色反應時間.
	public int[] Feature = new int[0]; // 特性列表
}

public class SavePlayer
{
	public int iStage = 0; // 關卡編號
	public int iStyle = 0; // 關卡風格編號
	public int iCurrency = 0; // 通貨
	public int iBattery = 0; // 電池
	public int iLightAmmo = 0; // 輕型彈藥
	public int iHeavyAmmo = 0; // 重型彈藥
	public int iBomb = 0; // 絕招次數
	public int iStamina = 0; // 耐力
	public int iDamageLv = 0; // 傷害等級
	public int iPlayTime = 0; // 遊戲時間
	public int iEnemyKill = 0; // 殺怪數量
	public int iPlayerLost = 0; // 死人數量
	public int iAdsWatch = 0; // 觀看廣告次數
	public SaveMember[] Party = new SaveMember[0]; // 成員列表
	public SaveMember[] Depot = new SaveMember[0]; // 成員列表
}

public class SaveEnemy
{
	public int iMonster = 0; // 怪物編號
	public int iHP = 0; // HP
	public float fMoveSpeed = 0.0f; // 移動速度
	public int iThreat = 0; // 威脅
	public float fPosX = 0.0f; // 座標X
	public float fPosY = 0.0f; // 座標Y
}

public class SaveGame
{
	public int iStageTime = 0; // 關卡時間.
	public int iKill = 0; // 殺怪數.
	public int iAlive = 0; // 存活數.
	public int iDead = 0; // 死亡數.
	public Pickup[] PickupList = new Pickup[0]; // 地圖拾取列表
	public int iRoad = 0; // 目前位置
	public bool bVictory = false; // 勝利旗標
    public int[] iWeaponType = new int[2];
    public int[] iWeaponIndex = new int[2];
}

public class SaveMap
{
	public MapCoor[] DataRoad = new MapCoor[0]; // 地圖道路列表
	public MapObjt[] DataObjt = new MapObjt[0]; // 地圖物件列表
}

public class SavePickup
{
	public Pickup[] Data = new Pickup[0]; // 拾取列表
}

public class SaveRecord : IEquatable<SaveRecord>, IComparable<SaveRecord>
{
	public int iStage = 0; // 關卡編號
	public int iPlayTime = 0; // 遊戲時間
	public int iEnemyKill = 0; // 殺怪數量
	public int iPlayerLost = 0; // 死人數量
	public string szTime = ""; // 紀錄時間

	public string RecordString()
	{
		return string.Format("{0:00000}_{1:00000}_{2:00000}_{3:00000}_{4}", iStage, 1.0f / iPlayTime, iEnemyKill, iPlayerLost, szTime);
	}
	public override int GetHashCode()
	{
		return RecordString().GetHashCode();
	}
	public override bool Equals(object obj)
	{
		return Equals(obj as SaveRecord);
	}
	public bool Equals(SaveRecord obj)
	{
		return obj != null && RecordString() == obj.RecordString();
	}
	public int CompareTo(SaveRecord obj)
	{
		if(obj == null)
			return 1;
		else
			return RecordString().CompareTo(obj.RecordString());
	}
}

public class SaveAchievement
{
	public int Key = 0; // 成就列舉
	public int Value = 0; // 值
}

public class SaveCollection
{
	public string[] Data = new string[0]; // 收集列表
}

public class SaveReward
{
	public int[] MemberLooks = new int[0]; // 角色外觀列表
	public int[] MemberInits = new int[0]; // 初始角色列表
	public string[] WeaponLevel = new string[0]; // 武器等級列表
	public int iInitCurrency = 0; // 初始金錢
	public int iInitBattery = 0; // 初始電池
	public int iInitLightAmmo = 0; // 初始輕型彈藥
	public int iInitHeavyAmmo = 0; // 初始重型彈藥
	public int iInitBomb = 0; // 初始絕招
	public int iCrystal = 0; // 水晶
}