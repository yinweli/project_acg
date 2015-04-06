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
		GameNew();
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

	// 新遊戲
	public void GameNew()
	{
		Data = new PlayerData();

		// 以下是測試資料, 以後要改
		Data.iStage = 1;
		Data.iBattery = 100;
		Data.iLightAmmo = 999;
		Data.iHeavyAmmo = 999;
		AddMember(new Looks(), 1);
		AddMember(new Looks(), 2);
		AddMember(new Looks(), 3);

		GameCalculate();
		GameSave();
	}
	// 讀取遊戲
	public void GameLoad()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSave))
		{
			Data = Json.ToObject<PlayerData>(PlayerPrefs.GetString(GameDefine.szSave));
			GameCalculate();
		}
		else
			GameNew();
	}
	// 儲存遊戲
	public void GameSave()
	{
		PlayerPrefs.SetString(GameDefine.szSave, Json.ToString(Data));
		PlayerPrefs.Save();
	}
	// 遊戲資料計算
	public void GameCalculate()
	{
		Rule.PressureReset();
		Rule.StaminaReset();
		Rule.StaminaLimit();
		Rule.StaminaRecovery();
		Rule.CriticalStrikeReset();
		Rule.AddDamageReset();
	}
	// 建立成員
	public void AddMember(Looks Looks, int iEquip)
	{
		Member Temp = new Member();

		Temp.Looks = Looks;
		Temp.iEquip = iEquip;

        Data.Data.Add(Temp);
	}
	// 刪除成員
	public void DelMember(int iPos)
	{
		Data.Data.RemoveAt(iPos);
	}
}