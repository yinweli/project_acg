using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataCollection : MonoBehaviour 
{
	static public DataCollection pthis = null;
	
	/* Save */
	public Dictionary<string, BitArray> Data = new Dictionary<string, BitArray>(); // 收集列表
	
	void Awake()
	{
		pthis = this;
	}
	private string BitArrayToString(BitArray Data)
	{
		string szTemp = "";
		
		foreach(bool Itor in Data)
			szTemp += Itor ? '1' : '0';
		
		return szTemp;
	}
	private BitArray StringToBitArray(string szData)
	{
		BitArray Temp = new BitArray(szData.Length);
		
		for(int iCount = 0; iCount < szData.Length; ++iCount)
			Temp[iCount] = szData[iCount] != '0';
		
		return Temp;
	}
	// 存檔.
	public void Save()
	{
		List<string> DataTemp = new List<string>();
		
		foreach(KeyValuePair<string, BitArray> Itor in Data)
			DataTemp.Add(Itor.Key + "_" + BitArrayToString(Itor.Value));
		
		SaveCollection Temp = new SaveCollection();
		
		Temp.Data = DataTemp.ToArray();
		
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
		{
			string[] szTemp = Itor.Split(new char[] {'_'});
			
			if(szTemp.Length >= 2)
				Data.Add(szTemp[0], StringToBitArray(szTemp[1]));
		}//for

		return true;
	}
	// 取得收集是否完成
	public bool IsComplete(int iGUID, int iLevel)
	{
		string szGUID = DBFCollection.GetGUID(iGUID, iLevel);

		return Data.ContainsKey(szGUID) ? Data[szGUID].Cast<bool>().Contains(false) == false : false;
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