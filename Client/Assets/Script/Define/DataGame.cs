using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class DataGame : MonoBehaviour 
{
    static public DataGame pthis = null;

    public ENUM_Language Language = ENUM_Language.enUS; // 現在使用的語系.

	/* Save */
    public int iStageTime = 0; // 關卡時間.
    public int iKill = 0; // 殺怪數.
    public int iAlive = 0; // 存活數.
    public int iDead = 0; // 死亡數.
	public int iRoad = 0; // 目前位置
	public bool bVictory = false; // 勝利旗標

    public int[] iWeaponType = new int[2];
    public int[] iWeaponIndex = new int[2];
        
	/* Not Save */
	public float fRunDouble = 1.0f; // 跑步速度倍率.
    
    void Awake()
    {
        pthis = this;
    }
	// 存檔.
	public void Save()
	{
		SaveGame Temp = new SaveGame();

		Temp.iStageTime = iStageTime;
		Temp.iKill = iKill;
		Temp.iAlive = iAlive;
		Temp.iDead = iDead;
		Temp.iRoad = iRoad;
		Temp.bVictory = bVictory;
        Temp.iWeaponType = iWeaponType;
        Temp.iWeaponIndex = iWeaponIndex;
		
		PlayerPrefs.SetString(GameDefine.szSaveGame, Json.ToString(Temp));
	}
    // 讀檔.
	public bool Load()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveGame) == false)
			return false;
		
		SaveGame Temp = Json.ToObject<SaveGame>(PlayerPrefs.GetString(GameDefine.szSaveGame));
		
		if(Temp == null)
			return false;

		iStageTime = Temp.iStageTime;
		iKill = Temp.iKill;
		iAlive = Temp.iAlive;
		iDead = Temp.iDead;
		iRoad = Temp.iRoad;
		bVictory = Temp.bVictory;
        iWeaponType = Temp.iWeaponType;
        iWeaponIndex = Temp.iWeaponIndex;

		return true;
	}
	// 清除資料
    public void Clear()
    {
        iStageTime = 0;
        iKill = 0;
        iAlive = 0;
        iDead = 0;
		iRoad = 0;
		bVictory = false;
        iWeaponType = new int[2];
        iWeaponIndex = new int[2];
    }
	// 清除存檔
	public void ClearSave()
	{
		Clear();
		PlayerPrefs.DeleteKey(GameDefine.szSaveGame);
	}
}