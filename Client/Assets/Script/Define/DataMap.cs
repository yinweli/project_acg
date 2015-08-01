using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataMap : MonoBehaviour
{
	static public DataMap pthis = null;

	/* Save */
	public List<MapCoor> DataRoad = new List<MapCoor>(); // 地圖道路列表
	public List<MapObjt> DataObjt = new List<MapObjt>(); // 地圖物件列表

	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		SaveMap Temp = new SaveMap();
		
		Temp.DataRoad = DataRoad.ToArray();
		Temp.DataObjt = DataObjt.ToArray();
		
		PlayerPrefs.SetString(GameDefine.szSaveMap, Json.ToString(Temp));
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveMap) == false)
			return false;
		
		SaveMap Temp = Json.ToObject<SaveMap>(PlayerPrefs.GetString(GameDefine.szSaveMap));
		
		if(Temp == null)
			return false;
		
		DataRoad = new List<MapCoor>(Temp.DataRoad);
		DataObjt = new List<MapObjt>(Temp.DataObjt);
		
		return true;
	}
	// 清除資料
	public void Clear()
	{
		DataRoad.Clear();
		DataObjt.Clear();
	}
	// 清除存檔
	public void ClearSave()
	{
		Clear();
		PlayerPrefs.DeleteKey(GameDefine.szSaveMap);
	}
}
