using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataAchievement : MonoBehaviour
{
	static public DataAchievement pthis = null;
	
	/* Save */
	public Dictionary<int, int> Data = new Dictionary<int, int>(); // 成就列表
	
	void Awake()
	{
		pthis = this;
	}
	// 存檔.
	public void Save()
	{
		List<string> DataTemp = new List<string>();
		
		foreach(KeyValuePair<int, int> Itor in Data)
			DataTemp.Add(Itor.Key + "_" + Itor.Value);
		
		SaveAchievement Temp = new SaveAchievement();
		
		Temp.Data = DataTemp.ToArray();
		
		PlayerPrefs.SetString(GameDefine.szSaveAchievement, Json.ToString(Temp));
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveAchievement) == false)
			return false;
		
		SaveAchievement Temp = Json.ToObject<SaveAchievement>(PlayerPrefs.GetString(GameDefine.szSaveAchievement));
		
		if(Temp == null)
			return false;
		
		foreach(string Itor in Temp.Data)
		{
			string[] szTemp = Itor.Split(new char[] {'_'});
			
			if(szTemp.Length >= 2)
				Data.Add(System.Convert.ToInt32(szTemp[0]), System.Convert.ToInt32(szTemp[1]));
		}//for
				
		return true;
	}
	// 清除單輪資料
	public void ClearSingle()
	{
		Data[(int)ENUM_Achievement.Single_Stage] = 0;
		Data[(int)ENUM_Achievement.Single_MemberSave] = 0;
		Data[(int)ENUM_Achievement.Single_MemberFire] = 0;
	}
	// 清除累積資料
	public void ClearTotal()
	{
		Data[(int)ENUM_Achievement.Total_Stage] = 0;
		Data[(int)ENUM_Achievement.Total_Crystal] = 0;
		Data[(int)ENUM_Achievement.Total_Currency] = 0;
		Data[(int)ENUM_Achievement.Total_Battery] = 0;
		Data[(int)ENUM_Achievement.Total_LightAmmo] = 0;
		Data[(int)ENUM_Achievement.Total_HeavyAmmo] = 0;
		Data[(int)ENUM_Achievement.Total_Bomb] = 0;
		Data[(int)ENUM_Achievement.Total_Kill] = 0;
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
		PlayerPrefs.DeleteKey(GameDefine.szSaveAchievement);
	}
}