﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

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
	public int iWidth = 0; // 地圖寬度
	public int iHeight = 0; // 地圖高度
	public int iStage = 0; // 關卡編號
	public int iStyle = 0; // 風格編號
	public int iRoad = 0; // 目前位置
	
	public bool IsEmpty()
	{
		return RoadList.Length <= 0 && ObjtList.Length <= 0;
	}
}

public class GameSave
{
	private static void GameSavePlayer()
	{
		SavePlayer Data = new SavePlayer();

		Data.iStage = SysMain.pthis.Data.iStage;
		Data.iCurrency = SysMain.pthis.Data.iCurrency;
		Data.iEnemyKill = SysMain.pthis.Data.iEnemyKill;
		Data.iPlayTime = SysMain.pthis.Data.iPlayTime;
		Data.Resource = SysMain.pthis.Data.Resource.ToArray();

		List<SaveMember> MemberList = new List<SaveMember>();
		
		foreach(Member Itor in SysMain.pthis.Data.Data)
		{
			SaveMember Temp = new SaveMember();
			
			Temp.Looks = Itor.Looks;
			Temp.iEquip = Itor.iEquip;
			Temp.Feature = Itor.Feature.ToArray();
			Temp.Behavior = Itor.Behavior.ToArray();
			
			MemberList.Add(Temp);
		}//for

		Data.Data = MemberList.ToArray();

		PlayerPrefs.SetString(GameDefine.szSavePlayer, Json.ToString(Data));
	}
	private static void GameSaveMap()
	{
		SaveMap Data = new SaveMap();

		List<MapRoad> RoadList = new List<MapRoad>();

		foreach(MapRoad Itor in MapCreater.This.RoadList)
		{
			MapRoad Temp = new MapRoad();

			Temp.Pos = Itor.Pos;
			Temp.Obj = null;

			RoadList.Add(Temp);
		}//for

		List<MapObjt> ObjtList = new List<MapObjt>();
		
		foreach(MapObjt Itor in MapCreater.This.ObjtList)
		{
			MapObjt Temp = new MapObjt();
			
			Temp.Pos = Itor.Pos;
			Temp.Type = Itor.Type;
			Temp.Width = Itor.Width;
			Temp.Height = Itor.Height;
			Temp.Obj = null;
			
			ObjtList.Add(Temp);
		}//for

		Data.RoadList = RoadList.ToArray();
		Data.ObjtList = ObjtList.ToArray();
		Data.iWidth = MapCreater.This.iWidth;
		Data.iHeight = MapCreater.This.iHeight;
		Data.iStage = MapCreater.This.iStage;
		Data.iStyle = MapCreater.This.iStyle;
		Data.iRoad = CameraCtrl.pthis.iNextRoad;

		PlayerPrefs.SetString(GameDefine.szSaveMap, Json.ToString(Data));
	}
	public static void Save()
	{
		GameSavePlayer();
		GameSaveMap();
		PlayerPrefs.Save();
	}
}

public class GameLoad
{
	private static bool GameLoadPlayer()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSavePlayer) == false)
			return false;

		SavePlayer Data = Json.ToObject<SavePlayer>(PlayerPrefs.GetString(GameDefine.szSavePlayer));

		if(Data == null)
			return false;

		SysMain.pthis.Data = new PlayerData();

		SysMain.pthis.Data.iStage = Data.iStage;
		SysMain.pthis.Data.iCurrency = Data.iCurrency;
		SysMain.pthis.Data.iEnemyKill = Data.iEnemyKill;
		SysMain.pthis.Data.iPlayTime = Data.iPlayTime;
		SysMain.pthis.Data.Resource = new List<int>(Data.Resource);
		SysMain.pthis.Data.Data = new List<Member>();

		foreach(SaveMember Itor in Data.Data)
		{
			Member MemberTemp = new Member();

			MemberTemp.Looks = Itor.Looks;
			MemberTemp.iEquip = Itor.iEquip;
			MemberTemp.Feature = new List<int>(Itor.Feature);
			MemberTemp.Behavior = new List<int>(Itor.Behavior);

			SysMain.pthis.Data.Data.Add(MemberTemp);
		}//for

		return true;
	}
	private static bool GameLoadMap()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveMap) == false)
			return false;

		SaveMap Data = Json.ToObject<SaveMap>(PlayerPrefs.GetString(GameDefine.szSaveMap));
		
		if(Data == null)
			return false;

		MapCreater.This.RoadList = new List<MapRoad>(Data.RoadList);
		MapCreater.This.ObjtList = new List<MapObjt>(Data.ObjtList);
		MapCreater.This.iWidth = Data.iWidth;
		MapCreater.This.iHeight = Data.iHeight;
		MapCreater.This.iStage = Data.iStage;
		MapCreater.This.iStyle = Data.iStyle;
		CameraCtrl.pthis.iNextRoad = Data.iRoad;

		return true;
	}
	public static void Load()
	{
		bool bResult = true;

		bResult &= GameLoadPlayer();
		bResult &= GameLoadMap();

		bResult = false;
		CameraCtrl.pthis.iNextRoad = 1;

		// 如果讀取失敗, 就建立新遊戲
		if(bResult == false)
		{
			SysMain.pthis.Data = new PlayerData();

			SysMain.pthis.Data.iStage = 1;
			SysMain.pthis.Data.iCurrency = 100;
			SysMain.pthis.Data.iEnemyKill = 0;
			SysMain.pthis.Data.iPlayTime = 0;

			// 以下是測試資料, 以後要改
			Rule.ResourceAdd(ENUM_Resource.Battery, 500);
			Rule.ResourceAdd(ENUM_Resource.LightAmmo, 999);
			Rule.ResourceAdd(ENUM_Resource.HeavyAmmo, 999);
			// 以下是測試資料, 以後要改
			Rule.MemberAdd(new Looks(), 1);
			Rule.MemberAdd(new Looks(), 5);
			Rule.MemberAdd(new Looks(), 8);

			// 建立地圖
			MapCreater.This.Create(SysMain.pthis.Data.iStage, 0);
		}//if

		Rule.PressureReset();
		Rule.StaminaLimit();
		Rule.StaminaReset();
		Rule.StaminaRecovery();
		Rule.CriticalStrikeReset();
		Rule.AddDamageReset();

		Debug.Log(SysMain.pthis.Data.iStamina + ", " + SysMain.pthis.Data.iStaminaLimit + ", " + SysMain.pthis.Data.iStaminaRecovery);
	}
}