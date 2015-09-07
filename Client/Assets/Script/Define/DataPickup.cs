using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataPickup : MonoBehaviour
{
	static public DataPickup pthis = null;

	/* Save */
	public List<Pickup> Data = new List<Pickup>(); // 地圖拾取列表

	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		SavePickup Temp = new SavePickup();

		Temp.Data = Data.ToArray();

		PlayerPrefs.SetString(GameDefine.szSavePickup, Json.ToString(Temp));
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSavePickup) == false)
			return false;
		
		SavePickup Temp = Json.ToObject<SavePickup>(PlayerPrefs.GetString(GameDefine.szSavePickup));
		
		if(Temp == null)
			return false;

		Data = new List<Pickup>(Temp.Data);
		
		return true;
	}
	// 清除資料
	public void Clear()
	{
		Data.Clear();
	}
	// 清除存檔
	public void ClearSave()
	{
		Clear();
		PlayerPrefs.DeleteKey(GameDefine.szSavePickup);
	}
}
