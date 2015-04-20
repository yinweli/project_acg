using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour 
{
    static public GameData pthis = null;

	/* Save */
    public int iStageTime = 0; // 關卡時間.
    public int iKill = 0; // 殺怪數.
    public int iAlive = 0; // 存活數.
    public int iDead = 0; // 死亡數.

	public List<MapCoor> RoadList = new List<MapCoor>(); // 地圖道路列表
	public List<MapObjt> ObjtList = new List<MapObjt>(); // 地圖物件列表
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
		Data.RoadList = RoadList.ToArray();
		Data.ObjtList = ObjtList.ToArray();
		Data.PickupList = PickupList.ToArray();
		Data.iRoad = CameraCtrl.pthis.iNextRoad;
		
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
		RoadList = new List<MapCoor>(Data.RoadList);
		ObjtList = new List<MapObjt>(Data.ObjtList);
		PickupList = new List<Pickup>(Data.PickupList);
		CameraCtrl.pthis.iNextRoad = Data.iRoad;
		
		return true;
	}
    public void ClearData()
    {
        iStageTime = 0;
        iKill = 0;
        iAlive = 0;
        iDead = 0;
    }
}