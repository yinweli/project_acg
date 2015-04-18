﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class SysMain : MonoBehaviour 
{
    public static SysMain pthis = null;

    public bool bIsGaming = false;

    // 跑步是否為冷卻中
    public bool bCanRun = true;

    // 人物佇列.
    public Dictionary<GameObject, int> Role = new Dictionary<GameObject, int>();
    // 可抓人物佇列.
    public Dictionary<GameObject, int> CatchRole = new Dictionary<GameObject, int>();
    // 敵人佇列.
    public Dictionary<GameObject, int> Enemy = new Dictionary<GameObject, int>();

    bool bResult = true;

    public float fSaveTime = 0;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    void Start()
    {
        // 讀取遊戲
        bResult &= PlayerData.pthis.Load();
        bResult &= GameData.pthis.Load();

        // 測試用
        bResult = false;

        // 確認是否為新遊戲.
        if (bResult == false)
        {
            PlayerData.pthis.iStage = 1;
            PlayerData.pthis.iCurrency = 0;
            PlayerData.pthis.iEnemyKill = 0;
            PlayerData.pthis.iPlayTime = 0;
            PlayerData.pthis.Resource = new List<int>();
            PlayerData.pthis.Members = new List<Member>();

			// 給與初始資源
            Rule.ResourceAdd(ENUM_Resource.Battery, GameDefine.iInitBattery);
			Rule.ResourceAdd(ENUM_Resource.LightAmmo, GameDefine.iInitLightAmmo);
			Rule.ResourceAdd(ENUM_Resource.HeavyAmmo, GameDefine.iInitHeavyAmmo);

            // 以下是測試資料, 以後要改
            Rule.MemberAdd(1);
			Rule.MemberAdd(5);
			Rule.MemberAdd(10);
			GameData.pthis.iStyle = 1;
        }//if
        // 建立地圖.
        MapCreater.pthis.Create();
        // 開始行走
        CameraCtrl.pthis.LoginMove();
    }
    // ------------------------------------------------------------------
	void Update () 
    {
        // 記錄遊戲時間.
        if (bIsGaming && fSaveTime <= Time.time)
        {
            GameData.pthis.iStageTime += GameDefine.iSaveSec;
            fSaveTime = Time.time + GameDefine.iSaveSec;
        }
	}
    // ------------------------------------------------------------------
    // 準備開始遊戲.
    public void ReadyStart()
    {
        PlayerData.pthis.iStaminaLimit = Rule.StaminaLimit();
		Rule.StaminaReset();
		Rule.StaminaRecovery();
		Rule.CriticalStrikeReset();
		Rule.AddDamageReset();
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
        // 清空地圖.
        MapCreater.pthis.Clear();
        // 建立地圖.
        MapCreater.pthis.Create();
        // 清空物件.
        ClearObj();
        // UI初始化.
        P_UI.pthis.StartNew();
        // 新遊戲 - 淡出淡入天數後開始遊戲.
        if (!bResult)
        {
            SysUI.pthis.ShowDay();
            GameData.pthis.ClearData();
        }        
        // 鏡頭位置調整.
        CameraCtrl.pthis.StartNew();
        // 到數開始.
        StartCoroutine(CountStart());
    }
    // ------------------------------------------------------------------
    IEnumerator CountStart()
    {
        int iCount = 3;
        while (iCount > 0)
        {
            yield return new WaitForSeconds(1);
            iCount--;
        }
        bIsGaming = true;
        P_UI.pthis.StartRecoverSta();
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
    // ------------------------------------------------------------------
    public void Victory()
    {
        bIsGaming = false;
        //Time.timeScale = 0;
        GameObject pObj = SysUI.pthis.CreatePanel("Prefab/P_Victory");
        pObj.transform.localPosition = new Vector3(0, 0, -1000);
    }
    // ------------------------------------------------------------------
    void ClearObj()
    {
        // 刪除人物.
        foreach (KeyValuePair<GameObject, int> itor in Role)
            Destroy(itor.Key);
        // 清空人物佇列.
        Role.Clear();

        // 刪除 敵人.
        foreach (KeyValuePair<GameObject, int> itor in Enemy)
            Destroy(itor.Key);
        // 清空 敵人佇列.
        Enemy.Clear();
    }
}