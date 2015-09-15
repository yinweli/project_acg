using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataCollection : MonoBehaviour 
{
	static public DataCollection pthis = null;
	
	/* Save */
	public List<Collection> Data = new List<Collection>(); // 收集列表
	
	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		SaveCollection Temp = new SaveCollection();
		
		Temp.Data = new string[Data.Count];

		for(int iPos = 0; iPos < Data.Count; ++iPos)
			Temp.Data[iPos] = Data[iPos].ToStringData();
		
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
		
		foreach(string Itor in Temp.Data)
			Data.Add(new Collection(Itor));

		return true;
	}
	// 新增收集
	public void Add(ENUM_Weapon Weapon, int iLevel, int iIndex)
	{
		if(Weapon == ENUM_Weapon.Null || Weapon == ENUM_Weapon.Count)
			return;

		if(iLevel <= 0 || iLevel > GameDefine.iMaxCollectionLv)
			return;

		if(iIndex <= 0 || iIndex > GameDefine.iMaxCollectionCount)
			return;

		Data.Add(new Collection(Weapon, iLevel, iIndex));
	}
	// 刪除收集
	public void Del(ENUM_Weapon Weapon, int iLevel, int iIndex)
	{
		string szTemp = (new Collection(Weapon, iLevel, iIndex)).ToStringData();

		for(int iPos = 0; iPos < Data.Count; ++iPos)
		{
			if(Data[iPos].ToStringData() == szTemp)
			{
				Data.RemoveAt(iPos);
				return;
			}//if
		}//for
	}
	// 檢查收集是否存在
	public bool IsExist(ENUM_Weapon Weapon, int iLevel, int iIndex)
	{
		string szTemp = (new Collection(Weapon, iLevel, iIndex)).ToStringData();

		foreach(Collection Itor in Data)
		{
			if(Itor.ToStringData() == szTemp)
				return true;
		}//for

		return false;
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
		PlayerPrefs.DeleteKey(GameDefine.szSaveCollection);
	}
}