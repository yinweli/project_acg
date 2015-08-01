using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataCollection : MonoBehaviour 
{
	static public DataCollection pthis = null;
	
	/* Save */
	public HashSet<int> Data = new HashSet<int>(); // 收集物品列表
	
	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		SaveCollection Temp = new SaveCollection();

		Temp.Data = new int[Data.Count];
		Data.CopyTo(Temp.Data);
		
		PlayerPrefs.SetString(GameDefine.szSaveCollection, Json.ToString(Temp));
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveCollection) == false)
			return false;

		SaveCollection Temp = Json.ToObject<SaveCollection>(PlayerPrefs.GetString(GameDefine.szSaveCollection));
		
		if(Temp == null)
			return false;

		Data = new HashSet<int>(Temp.Data);
		
		return true;
	}
	// 清除資料
	public void Clear()
	{
		Data.Clear();
	}
}