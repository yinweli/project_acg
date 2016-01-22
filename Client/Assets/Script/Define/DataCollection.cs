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
	public bool Add(ENUM_Weapon Weapon, int iLevel, int iIndex)
	{
		if(Weapon == ENUM_Weapon.Null || Weapon == ENUM_Weapon.Count)
			return false;

		if(iLevel <= 0 || iLevel > GameDefine.iMaxCollectionLv)
			return false;

		if(iIndex <= 0 || iIndex > GameDefine.iMaxCollectionCount)
			return false;

		Collection Temp = new Collection(Weapon, iLevel, iIndex);
		string szTemp = Temp.ToStringData();

		if(Data.Any((Itor) => Itor.ToStringData() == szTemp))
			return false;

		Data.Add(new Collection(Weapon, iLevel, iIndex));

		return true;
	}
	// 新增收集
	public void Add(ENUM_Weapon Weapon, int iLevel)
	{
		for(int iIndex = 0; iIndex <= GameDefine.iMaxCollectionCount; ++iIndex)
			Add(Weapon, iLevel, iIndex);
	}
	// 刪除收集
	public void Del(ENUM_Weapon Weapon, int iLevel, int iIndex)
	{
		string szTemp = (new Collection(Weapon, iLevel, iIndex)).ToStringData();
		int iPos = Data.FindIndex((Itor) => Itor.ToStringData() == szTemp);

		if(iPos >= 0 && iPos < Data.Count)
			Data.RemoveAt(iPos);
	}
	// 檢查收集是否存在
	public bool IsExist(ENUM_Weapon Weapon, int iLevel, int iIndex)
	{
		string szTemp = (new Collection(Weapon, iLevel, iIndex)).ToStringData();

		return Data.Any((Itor) => Itor.ToStringData() == szTemp);
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