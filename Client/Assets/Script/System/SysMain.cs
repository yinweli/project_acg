using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class SysMain : MonoBehaviour 
{
    public static SysMain pthis = null;

    public bool bIsGaming = true;

    public PlayerData Data = new PlayerData();
    // 人物佇列.
    public Dictionary<GameObject, int> Role = new Dictionary<GameObject, int>();
    // 敵人佇列.
    public Dictionary<GameObject, int> Enemy = new Dictionary<GameObject, int>();
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }

    void OnDestroy()
    {
        //pthis = null;
    }
    // ------------------------------------------------------------------
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 存檔
	public void GameSave()
	{
		PlayerPrefs.SetString(GameDefine.szSave, Json.ToString(Data));
		PlayerPrefs.Save();
		Debug.Log("game save");
	}
	// 讀檔
	public void GameLoad()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSave))
			Data = Json.ToObject<PlayerData>(PlayerPrefs.GetString(GameDefine.szSave));
		else
		{
			// 沒有遊戲紀錄, 建立一個新的
			Data = new PlayerData();

			// 以下是測試資料, 以後要改
			Data.iStage = 1;
			Data.iBattery = 100;
			Data.iLightAmmo = 999;
			Data.iHeavyAmmo = 999;
			AddMember(new Looks(), 1);
			AddMember(new Looks(), 2);
			AddMember(new Looks(), 3);
		}//if

		Debug.Log("game load");
	}
	// 建立成員
	public Member AddMember(Looks Looks, int iEquip)
	{
		Member Data = new Member();

		Data.Looks = Looks;
		Data.iEquip = iEquip;

		return Data;
	}
}