using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour 
{
    static public GameData pthis = null;

    public ENUM_Language Language = ENUM_Language.enUS; // 現在使用的語系.
	/* Save */
    public int iStageTime = 0; // 關卡時間.
    public int iKill = 0; // 殺怪數.
    public int iAlive = 0; // 存活數.
    public int iDead = 0; // 死亡數.
	public int iRoad = 0; // 目前位置
	public bool bVictory = false; // 勝利旗標

	public List<Pickup> PickupList = new List<Pickup>(); // 地圖拾取列表

	/* Not Save */
	public float fRunDouble = 1.0f; // 跑步速度倍率.
    
    void Awake()
    {
        pthis = this;
    }
	// 存檔.
	public void Save()
	{
		SaveGame Data = new SaveGame();

		Data.iStageTime = iStageTime;
		Data.iKill = iKill;
		Data.iAlive = iAlive;
		Data.iDead = iDead;
		Data.PickupList = PickupList.ToArray();
		Data.iRoad = iRoad;
		Data.bVictory = bVictory;
		
		PlayerPrefs.SetString(GameDefine.szSaveGame, Json.ToString(Data));
	}
    // 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveGame) == false)
			return false;
		
		SaveGame Data = Json.ToObject<SaveGame>(PlayerPrefs.GetString(GameDefine.szSaveGame));
		
		if(Data == null)
			return false;

		iStageTime = Data.iStageTime;
		iKill = Data.iKill;
		iAlive = Data.iAlive;
		iDead = Data.iDead;
		iRoad = Data.iRoad;
		bVictory = Data.bVictory;
		PickupList = new List<Pickup>(Data.PickupList);
		
		return true;
	}
	// 清除資料
    public void ClearData()
    {
        iStageTime = 0;
        iKill = 0;
        iAlive = 0;
        iDead = 0;
		iRoad = 0;
		bVictory = false;
		PickupList.Clear();
    }
	// 清除存檔
	public void Clear()
	{
		ClearData();
		PlayerPrefs.DeleteKey(GameDefine.szSaveGame);
	}
}