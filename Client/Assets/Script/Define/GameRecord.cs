using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class GameSave
{
	private static void GameSavePlayer()
	{
		SavePlayer Data = new SavePlayer();

		Data.iStage = PlayerData.pthis.iStage;
		Data.iCurrency = PlayerData.pthis.iCurrency;
		Data.iEnemyKill = PlayerData.pthis.iEnemyKill;
		Data.iPlayTime = PlayerData.pthis.iPlayTime;
		Data.Resource = PlayerData.pthis.Resource.ToArray();

		List<SaveMember> MemberList = new List<SaveMember>();
		
		foreach(Member Itor in PlayerData.pthis.Members)
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

		foreach(MapRoad Itor in GameData.pthis.RoadList)
		{
			MapRoad Temp = new MapRoad();

			Temp.Pos = Itor.Pos;
			Temp.Obj = null;

			RoadList.Add(Temp);
		}//for

		List<MapObjt> ObjtList = new List<MapObjt>();
		
		foreach(MapObjt Itor in GameData.pthis.ObjtList)
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

		PlayerData.pthis = new PlayerData();

		PlayerData.pthis.iStage = Data.iStage;
		PlayerData.pthis.iCurrency = Data.iCurrency;
		PlayerData.pthis.iEnemyKill = Data.iEnemyKill;
		PlayerData.pthis.iPlayTime = Data.iPlayTime;
		PlayerData.pthis.Resource = new List<int>(Data.Resource);
		PlayerData.pthis.Members = new List<Member>();

		foreach(SaveMember Itor in Data.Data)
		{
			Member MemberTemp = new Member();

			MemberTemp.Looks = Itor.Looks;
			MemberTemp.iEquip = Itor.iEquip;
			MemberTemp.Feature = new List<int>(Itor.Feature);
			MemberTemp.Behavior = new List<int>(Itor.Behavior);

			PlayerData.pthis.Members.Add(MemberTemp);
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

		GameData.pthis.RoadList = new List<MapRoad>(Data.RoadList);
		GameData.pthis.ObjtList = new List<MapObjt>(Data.ObjtList);
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
			PlayerData.pthis = new PlayerData();

			PlayerData.pthis.iStage = 1;
			PlayerData.pthis.iCurrency = 100;
			PlayerData.pthis.iEnemyKill = 0;
			PlayerData.pthis.iPlayTime = 0;

			// 以下是測試資料, 以後要改
			GameData.pthis.iStyle = 1;
			Rule.ResourceAdd(ENUM_Resource.Battery, 500);
			Rule.ResourceAdd(ENUM_Resource.LightAmmo, 999);
			Rule.ResourceAdd(ENUM_Resource.HeavyAmmo, 999);
			// 以下是測試資料, 以後要改
			Rule.MemberAdd(new Looks(), 1);
			Rule.MemberAdd(new Looks(), 5);
			Rule.MemberAdd(new Looks(), 8);
		}//if

		Rule.StaminaLimit();
		Rule.StaminaReset();
		Rule.StaminaRecovery();
		Rule.CriticalStrikeReset();
		Rule.AddDamageReset();

		Debug.Log(PlayerData.pthis.iStamina + ", " + PlayerData.pthis.iStaminaLimit + ", " + PlayerData.pthis.iStaminaRecovery);
	}
}