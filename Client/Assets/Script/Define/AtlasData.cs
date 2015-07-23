using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class AtlasData : MonoBehaviour 
{
	static public AtlasData pthis = null;
	
	/* Save */
	public Dictionary<int, BitArray> Atlas = new Dictionary<int, BitArray>(); // 圖鑑列表
	
	void Awake()
	{
		pthis = this;
	}
	string BitArrayToString(BitArray Data)
	{
		string szTemp = "";

		foreach(bool Itor in Data)
			szTemp += Itor ? '1' : '0';

		return szTemp;
	}
	BitArray StringToBitArray(string szData)
	{
		BitArray Temp = new BitArray(szData.Length);

		for(int iCount = 0; iCount < szData.Length; ++iCount)
			Temp[iCount] = szData[iCount] == '0' ? false : true;

		return Temp;
	}
	// 存檔.
	public void Save()
	{
		List<string> AtlasTemp = new List<string>();

		foreach(KeyValuePair<int, BitArray> Itor in Atlas)
			AtlasTemp.Add(Itor.Key + "_" + BitArrayToString(Itor.Value));

		SaveAtlas Data = new SaveAtlas();

		Data.Data = AtlasTemp.ToArray();
		
		PlayerPrefs.SetString(GameDefine.szSaveAtlas, Json.ToString(Data));
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveAtlas) == false)
			return false;
		
		SaveAtlas Data = Json.ToObject<SaveAtlas>(PlayerPrefs.GetString(GameDefine.szSaveAtlas));
		
		if(Data == null)
			return false;

		foreach(string Itor in Data.Data)
		{
			string[] szTemp = Itor.Split(new char[] {'_'});

			if(szTemp.Length >= 2)
				Atlas.Add(System.Convert.ToInt32(szTemp[0]), StringToBitArray(szTemp[1]));
		}//for
		
		return true;
	}
	// 清除資料
	public void ClearData()
	{
		Atlas.Clear();
	}
}