using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class SysMain : MonoBehaviour 
{
    public static SysMain pthis = null;

    public bool bIsGaming = true;

    public PlayerData Data = new PlayerData();

    public int iRoleCount = 0;
    public int iEnemyCount = 0;
    // 跑步速度倍率.
    public float fRunDouble = 1;
    // 耐力消耗值.
    public int iStaminaCost= 5;
    // 跑步是否為冷卻中
    public bool bCanRun = true;

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
	// Update is called once per frame
	void Update () 
    {
        iRoleCount = Role.Count;
        iEnemyCount = Enemy.Count;
	}
    // ------------------------------------------------------------------
	// 新遊戲
	public void GameNew()
	{
		Data = new PlayerData();

        iStaminaCost = GameDefine.iStaminaConsume;
        Data.iStaminaLimit = GameDefine.iStaminaLimit;
        Data.iStaminaRecovery = GameDefine.iStaminaRecovery;

		// 以下是測試資料, 以後要改
		Data.iStage = 1;
        Rule.ResourceAdd(ENUM_Resource.Battery, 500);
		Rule.ResourceAdd(ENUM_Resource.LightAmmo, 999);
		Rule.ResourceAdd(ENUM_Resource.HeavyAmmo, 999);
		AddMember(new Looks(), 1);
		AddMember(new Looks(), 5);
		AddMember(new Looks(), 8);

		GameCalculate();
		GameSave();
	}
    // ------------------------------------------------------------------
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
    // ------------------------------------------------------------------
	// 儲存遊戲
	public void GameSave()
	{
		PlayerPrefs.SetString(GameDefine.szSave, Json.ToString(Data));
		PlayerPrefs.Save();
	}
    // ------------------------------------------------------------------
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
    // ------------------------------------------------------------------
	// 建立成員
	public void AddMember(Looks Looks, int iEquip)
	{
		Member Temp = new Member();

		Temp.Looks = Looks;
		Temp.iEquip = iEquip;

        Data.Data.Add(Temp);
	}
    // ------------------------------------------------------------------
	// 刪除成員
	public void DelMember(int iPos)
	{
		Data.Data.RemoveAt(iPos);
	}
    // ------------------------------------------------------------------
    public float GetMoveSpeed()
    {
        return GameDefine.fBaseSpeed * fRunDouble;
    }
    // ------------------------------------------------------------------
    // 增加耐力.
    public bool AddStamina(int iValue)
    {
        if (iValue < 0 && !bCanRun)
            return false;

        // 當扣除後低於0進入冷卻狀態.
        if (bCanRun && SysMain.pthis.Data.iStamina + iValue <= 0)
        {
            SysMain.pthis.Data.iStamina = 0;
            SysMain.pthis.fRunDouble = 0;
            bCanRun = false;
            P_UI.pthis.UpdateStamina();
            return true;
        }
        
        Rule.StaminaAdd(iValue);

        if (iValue > 0 && !bCanRun && SysMain.pthis.Data.iStamina + iValue > 20)
            bCanRun = true;
        if (iValue > 0 && !bCanRun && SysMain.pthis.Data.iStamina + iValue > 10)
            SysMain.pthis.fRunDouble = 1.0f;

        P_UI.pthis.UpdateStamina();
        return true;
    }
}