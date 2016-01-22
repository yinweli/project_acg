using UnityEngine;
using LibCSNStandard;
using System;
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
		int iCount = 0;

		foreach(KeyValuePair<int, int> Itor in Data)
		{
			SaveAchievement Temp = new SaveAchievement();

			Temp.Key = Itor.Key;
			Temp.Value = Itor.Value;

			PlayerPrefs.SetString(GameDefine.szSaveAchievement + iCount, Json.ToString(Temp));
			++iCount;
		}//for

		PlayerPrefs.SetInt(GameDefine.szSaveAchievementCount, iCount);
	}
	// 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveAchievementCount) == false)
			return false;

		for(int iPos = 0, iMax = PlayerPrefs.GetInt(GameDefine.szSaveAchievementCount); iPos < iMax; ++iPos)
		{
			string szSave = GameDefine.szSaveAchievement + iPos;
			
			if(PlayerPrefs.HasKey(szSave) == false)
				continue;

			SaveAchievement Temp = Json.ToObject<SaveAchievement>(PlayerPrefs.GetString(szSave));

			if(Temp == null)
				continue;

			Data.Add(Temp.Key, Temp.Value);
		}//for
		
		return true;
	}
	// 取得成就值
	public int GetValue(ENUM_Achievement emAchievement)
	{
		int iGUID = (int)emAchievement;

		return Data.ContainsKey(iGUID) ? Data[iGUID] : 0;
	}
    // 更動成就值.
    public void SetValue(ENUM_Achievement emAchievement, int iValue)
    {
        int iGUID = (int)emAchievement;

        if (!Data.ContainsKey(iGUID))
            Data.Add(iGUID, iValue);
        else
            Data[iGUID] = iValue;
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
		PlayerPrefs.DeleteKey(GameDefine.szSaveAchievementCount);

		foreach(ENUM_Achievement Itor in Enum.GetValues(typeof(ENUM_Achievement)))
			PlayerPrefs.DeleteKey(GameDefine.szSaveAchievement + (int)Itor);
	}
}