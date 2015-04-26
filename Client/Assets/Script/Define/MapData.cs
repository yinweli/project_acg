using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class MapData : MonoBehaviour
{
	static public MapData pthis = null;

	/* Save */
	public List<MapCoor> RoadList = new List<MapCoor>(); // 地圖道路列表
	public List<MapObjt> ObjtList = new List<MapObjt>(); // 地圖物件列表

	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		SaveMap Data = new SaveMap();
		
		Data.RoadList = RoadList.ToArray();
		Data.ObjtList = ObjtList.ToArray();
		
		PlayerPrefs.SetString(GameDefine.szSaveMap, Json.ToString(Data));
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveMap) == false)
			return false;
		
		SaveMap Data = Json.ToObject<SaveMap>(PlayerPrefs.GetString(GameDefine.szSaveMap));
		
		if(Data == null)
			return false;
		
		RoadList = new List<MapCoor>(Data.RoadList);
		ObjtList = new List<MapObjt>(Data.ObjtList);
		
		return true;
	}
	// 清除資料
	public void ClearData()
	{
		RoadList.Clear();
		ObjtList.Clear();
	}
	// 清除存檔
	public void Clear()
	{
		ClearData();
		PlayerPrefs.DeleteKey(GameDefine.szSaveMap);
	}
}
