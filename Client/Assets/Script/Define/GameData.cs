using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour 
{
    static public GameData pthis = null;

	/* Save */
    public int iStageTime = 0; // 關卡時間.
    public int iKill = 0; // 殺怪數.
    public int iAlive = 0; // 存活數.
    public int iDead = 0; // 死亡數.

	public List<MapRoad> RoadList = new List<MapRoad>(); // 地圖道路列表
	public List<MapObjt> ObjtList = new List<MapObjt>(); // 地圖物件列表
	public List<Pickup> PickupList = new List<Pickup>(); // 地圖拾取列表

	/* Not Save */
	public int iStyle = 1; // 風格編號
	public float fRunDouble = 1.0f; // 跑步速度倍率.
    
    void Awake()
    {
        pthis = this;
    }
	// 存檔.
	public void Save()
	{
		SaveGame Data = new SaveGame();

		Data.iStageTime = iStageTime;
		Data.iKill = iKill;
		Data.iAlive = iAlive;
		Data.iDead = iDead;
		
		List<MapRoad> TempRoadList = new List<MapRoad>();
		
		foreach(MapRoad Itor in RoadList)
		{
			MapRoad Temp = new MapRoad();
			
			Temp.Pos = Itor.Pos;
			Temp.Obj = null;
			
			TempRoadList.Add(Temp);
		}//for
		
		List<MapObjt> TempObjtList = new List<MapObjt>();
		
		foreach(MapObjt Itor in ObjtList)
		{
			MapObjt Temp = new MapObjt();
			
			Temp.Pos = Itor.Pos;
			Temp.Type = Itor.Type;
			Temp.Width = Itor.Width;
			Temp.Height = Itor.Height;
			Temp.Obj = null;
			
			TempObjtList.Add(Temp);
		}//for

		Data.RoadList = TempRoadList.ToArray();
		Data.ObjtList = TempObjtList.ToArray();
		Data.iRoad = CameraCtrl.pthis.iNextRoad;
		
		PlayerPrefs.SetString(GameDefine.szSaveGame, Json.ToString(Data));
	}
    // 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveGame) == false)
			return false;
		
		SaveGame Data = Json.ToObject<SaveGame>(PlayerPrefs.GetString(GameDefine.szSaveGame));
		
		if(Data == null)
			return false;

		iStageTime = Data.iStageTime;
		iKill = Data.iKill;
		iAlive = Data.iAlive;
		iDead = Data.iDead;
		RoadList = new List<MapRoad>(Data.RoadList);
		ObjtList = new List<MapObjt>(Data.ObjtList);
		CameraCtrl.pthis.iNextRoad = Data.iRoad;
		
		return true;
	}
    public void ClearData()
    {
        iStageTime = 0;
        iKill = 0;
        iAlive = 0;
        iDead = 0;
    }
}
