using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Member
{
	/* [Save] */ public int iSex = 0; // 性別編號
	/* [Save] */ public int iLook = 0; // 外觀編號
	/* [Save] */ public int iEquip = 0; // 裝備編號
	/* [Save] */ public List<int> Feature = new List<int>(); // 特性列表
	/* [Save] */ public List<int> Behavior = new List<int>(); // 行為列表
	/* [    ] */ public int iInvincibleTime = 0; // 無敵時間
	/* [    ] */ public float fCriticalStrike = 0.0f; // 致命
	/* [    ] */ public int iAddDamage = 0; // 增傷
}

public class SaveMember
{
	public int iSex = 0; // 性別編號
	public int iLook = 0; // 外觀編號
	public int iEquip = 0; // 裝備編號
	public int[] Feature = new int[0]; // 特性列表
	public int[] Behavior = new int[0]; // 行為列表
}

public class SavePlayer
{
	public int iStage = 0; // 關卡編號
	public int iCurrency = 0; // 通貨
	public int iEnemyKill = 0; // 殺怪數量
	public int iPlayTime = 0; // 遊戲時間
	public int[] Resource = new int[0]; // 資源列表
	public SaveMember[] Data = new SaveMember[0]; // 成員列表
}

public class SaveMap
{
	public MapRoad[] RoadList = new MapRoad[0]; // 地圖道路列表
	public MapObjt[] ObjtList = new MapObjt[0]; // 地圖物件列表
	public int iRoad = 0; // 目前位置
	
	public bool IsEmpty()
	{
		return RoadList.Length <= 0 && ObjtList.Length <= 0;
	}
}