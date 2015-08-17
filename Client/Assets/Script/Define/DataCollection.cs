using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataCollection : MonoBehaviour 
{
	static public DataCollection pthis = null;
	
	/* Save */
	public HashSet<int> Data = new HashSet<int>(); // 收集列表

	/* Not Save */
	public HashSet<int> Collection = new HashSet<int>(); // 收集物品列表, 用來比對物品是否可以收集
	
	void Awake()
	{
		pthis = this;
	}
	void Start()
	{
		DBFItor Itor = GameDBF.pthis.GetCollection();
		
		while(Itor.IsEnd() == false)
		{
			DBFCollection DBFTemp = (DBFCollection)Itor.Data();
			
			for(int iLevel = GameDefine.iMinCollectionLv; iLevel <= GameDefine.iMaxCollectionLv; ++iLevel)
			{
				foreach(int ItorItems in Rule.CollectionIndex(System.Convert.ToInt32(DBFTemp.GUID), iLevel))
					Collection.Add(ItorItems);
			}//for
			
			Itor.Next();
		}//while

		Debug.Log("Collection list have " + Collection.Count + " items");
	}
	// 存檔.
	public void Save()
	{
		SaveCollection Temp = new SaveCollection();
		
		Temp.Data = Data.ToList().ToArray();
		
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
		
		foreach(int Itor in Temp.Data)
			Data.Add(Itor);

		return true;
	}
	// 檢查列表內物品是否都收集了
	public bool IsComplete(List<int> Items)
	{
		bool bResult = true;

		foreach(int Itor in Items)
			bResult &= Data.Contains(Itor);

		return bResult;
	}
	// 取得比對後已收集的物品列表
	public List<int> GetComplete(List<int> Items)
	{
		List<int> Result = new List<int>();

		foreach(int Itor in Items)
		{
			if(Data.Contains(Itor))
				Result.Add(Itor);
		}//for

		return Result;
	}
	// 檢查物品是否存在收集列表中
	public bool IsCollection(int iItems)
	{
		return Collection.Contains(iItems);
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