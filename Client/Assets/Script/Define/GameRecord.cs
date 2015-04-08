using UnityEngine;
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
	
	public bool IsEmpty()
	{
		return RoadList.Length <= 0 && ObjtList.Length <= 0;
	}
}

public class GameSave
{
	public const string szSavePlayer = "save_player";
	public const string szSaveMap = "save_map";

	private void GameSavePlayer(PlayerData Data)
	{
		SavePlayer Temp = new SavePlayer();

		Temp.iStage = Data.iStage;
		Temp.iCurrency = Data.iCurrency;
		Temp.iEnemyKill = Data.iEnemyKill;
		Temp.iPlayTime = Data.iPlayTime;
		Temp.Resource = Data.Resource.ToArray();

		List<SaveMember> MemberList = new List<SaveMember>();
		
		foreach(Member Itor in Data.Data)
		{
			SaveMember MemberTemp = new SaveMember();
			
			MemberTemp.Looks = Itor.Looks;
			MemberTemp.iEquip = Itor.iEquip;
			MemberTemp.Feature = Itor.Feature.ToArray();
			MemberTemp.Behavior = Itor.Behavior.ToArray();
			
			MemberList.Add(MemberTemp);
		}//for

		Temp.Data = MemberList.ToArray();

		PlayerPrefs.SetString(szSavePlayer, Json.ToString(Temp));
	}
	private void GameSaveMap(MapCreater Data)
	{
		SaveMap Temp = new SaveMap();

		Temp.RoadList = Data.RoadList.ToArray();
		Temp.ObjtList = Data.ObjtList.ToArray();
		Temp.iWidth = Data.iWidth;
		Temp.iHeight = Data.iHeight;
		Temp.iStage = Data.iStage;
		Temp.iStyle = Data.iStyle;

		PlayerPrefs.SetString(szSaveMap, Json.ToString(Temp));
	}
	public void Save()
	{
		GameSavePlayer(SysMain.pthis.Data);
		GameSaveMap(MapCreater.This);
		PlayerPrefs.Save();
	}
}

public class GameLoad
{

}