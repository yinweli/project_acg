using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Looks
{
	/* [Save] */ public int iHair = 0; // 頭髮
	/* [Save] */ public int iDecorative = 0; // 裝飾
	/* [Save] */ public int iFace = 0; // 臉部
	/* [Save] */ public int iEyes = 0; // 眼睛
	/* [Save] */ public int iEyeback = 0; // 眼白
	/* [Save] */ public int iEyebrow = 0; // 睫毛
	/* [Save] */ public int iBlush = 0; // 腮紅
	/* [Save] */ public int iMonth = 0; // 嘴巴
	/* [Save] */ public int iBody = 0; // 身體
	/* [Save] */ public int iFoot = 0; // 腳
	/* [Save] */ public int iHand = 0; // 手
}

public class Member
{
	/* [Save] */ public Looks Looks = new Looks(); // 外型資料
	/* [Save] */ public int iEquip = 0; // 裝備編號
	/* [Save] */ public List<int> Feature = new List<int>(); // 特性列表
	/* [Save] */ public List<int> Behavior = new List<int>(); // 行為列表
	/* [    ] */ public int iInvincibleTime = 0; // 無敵時間
	/* [    ] */ public float fCriticalStrike = 0.0f; // 致命
	/* [    ] */ public int iAddDamage = 0; // 增傷
}

public class SaveMember
{
	public Looks Looks = new Looks(); // 外型資料
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