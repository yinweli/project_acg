using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour 
{
    static public GameData pthis = null;

	/* Save */
	public List<MapRoad> RoadList = new List<MapRoad>(); // 地圖道路列表
	public List<MapObjt> ObjtList = new List<MapObjt>(); // 地圖物件列表

	/* Not Save */
	public int iStyle = 1; // 風格編號
	public float fRunDouble = 1.0f; // 跑步速度倍率.

    void Awake()
    {
        pthis = this;
    }

    // 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveMap) == false)
			return false;
		
		SaveMap Data = Json.ToObject<SaveMap>(PlayerPrefs.GetString(GameDefine.szSaveMap));
		
		if(Data == null)
			return false;
		
		RoadList = new List<MapRoad>(Data.RoadList);
		ObjtList = new List<MapObjt>(Data.ObjtList);
		CameraCtrl.pthis.iNextRoad = Data.iRoad;
		
		return true;
	}

	// 存檔.
	public void Save()
	{
		SaveMap Data = new SaveMap();
		
		List<MapRoad> RoadList = new List<MapRoad>();
		
		foreach(MapRoad Itor in RoadList)
		{
			MapRoad Temp = new MapRoad();
			
			Temp.Pos = Itor.Pos;
			Temp.Obj = null;
			
			RoadList.Add(Temp);
		}//for
		
		List<MapObjt> ObjtList = new List<MapObjt>();
		
		foreach(MapObjt Itor in ObjtList)
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
}
