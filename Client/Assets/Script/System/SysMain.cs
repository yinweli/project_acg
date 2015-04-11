using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class SysMain : MonoBehaviour 
{
    public static SysMain pthis = null;

    public bool bIsGaming = false;

    // 角色數量
    public int iRoleCount = 0;
	// 怪物數量
    public int iEnemyCount = 0;
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
    // 準備開始遊戲.
    public void ReadyStart()
    {
		bool bResult = true;

		// 讀取遊戲
		bResult &= PlayerData.pthis.Load();
		bResult &= GameData.pthis.Load();

		// 測試用
		bResult = false;

		// 確認是否為新遊戲.
		if(bResult == false)
		{
			PlayerData.pthis.iStage = 1;
			PlayerData.pthis.iCurrency = 100;
			PlayerData.pthis.iEnemyKill = 0;
			PlayerData.pthis.iPlayTime = 0;
			PlayerData.pthis.Resource = new List<int>();
			PlayerData.pthis.Members = new List<Member>();

			// 以下是測試資料, 以後要改
			GameData.pthis.iStyle = 1;
			Rule.ResourceAdd(ENUM_Resource.Battery, 500);
			Rule.ResourceAdd(ENUM_Resource.LightAmmo, 999);
			Rule.ResourceAdd(ENUM_Resource.HeavyAmmo, 999);
			// 以下是測試資料, 以後要改
			Rule.MemberAdd(new Looks(), 1);
			Rule.MemberAdd(new Looks(), 5);
			Rule.MemberAdd(new Looks(), 8);

			// 建立地圖.
			MapCreater.pthis.Create();
		}//if

		Rule.StaminaLimit();
		Rule.StaminaReset();
		Rule.StaminaRecovery();
		Rule.CriticalStrikeReset();
		Rule.AddDamageReset();
        MapMove.pthis.StartNew();

        // 新遊戲 - 淡出淡入天數後開始遊戲.
        SysUI.pthis.ShowDay();

        NewGame();
    }
	// ------------------------------------------------------------------
	// 儲存遊戲.
	public void SaveGame()
	{
		PlayerData.pthis.Save();
		GameData.pthis.Save();
		PlayerPrefs.Save();
	}
    // ------------------------------------------------------------------
    // 開始遊戲.
    public void NewGame()
    {
        bIsGaming = true;
        // 創建人物.
        PlayerCreater.pthis.StartNew();
        // 開始出怪.
        EnemyCreater.pthis.StartNew();
    }
    // ------------------------------------------------------------------
    // 取得真實跑速
    public float GetMoveSpeed()
    {
        return GameDefine.fBaseSpeed * GameData.pthis.fRunDouble;
    }
    // ------------------------------------------------------------------
    // 增加耐力.
    public bool AddStamina(int iValue)
    {
        if (iValue < 0 && !bCanRun)
            return false;

        // 當扣除後低於0進入冷卻狀態.
        if (bCanRun && PlayerData.pthis.iStamina + iValue <= 0)
        {
            PlayerData.pthis.iStamina = 0;
            GameData.pthis.fRunDouble = 0;
            bCanRun = false;
            P_UI.pthis.UpdateStamina();
            return true;
        }
        
        Rule.StaminaAdd(iValue);

        if (iValue > 0 && !bCanRun && PlayerData.pthis.iStamina + iValue > 20)
            bCanRun = true;
        if (iValue > 0 && !bCanRun && PlayerData.pthis.iStamina + iValue > 10)
            GameData.pthis.fRunDouble = 1.0f;

        P_UI.pthis.UpdateStamina();
        return true;
    }
}